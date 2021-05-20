using System.Threading;
using System.Threading.Tasks;
using GoLocal.Domain.Entities;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Artisan.Application.Queries.Invoices.GetInvoice
{
    public class GetInvoiceQueryHandler : AbstractRequestHandler<GetInvoiceQuery, GetInvoiceResponse>
    {
        private readonly Context _context;

        public GetInvoiceQueryHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result<GetInvoiceResponse>> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            GetInvoiceResponse invoice = await _context.Invoices
                .Include(m => m.User)
                .Include(m => m.InvoiceItems).ThenInclude(m => m.Comment)
                .Include(m => m.InvoiceItems).ThenInclude(m => m.Package)
                .ProjectToType<GetInvoiceResponse>()
                .SingleOrDefaultAsync(m => m.Id == request.InvoiceId, cancellationToken);
            
            return invoice == null ? NotFound<Invoice>(request.InvoiceId) : Ok(invoice);
        }
    }
}