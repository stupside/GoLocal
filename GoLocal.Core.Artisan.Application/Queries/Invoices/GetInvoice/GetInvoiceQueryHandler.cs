using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Persistence.EntityFramework;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Artisan.Application.Queries.Invoices.GetInvoice
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