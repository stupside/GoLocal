using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Commons.Filtering;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Bus.Results.Pages;
using GoLocal.Core.Artisan.Application.Queries.Invoices.GetInvoices.Models;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Artisan.Application.Queries.Invoices.GetInvoices
{
    public class GetInvoicesQueryHandler : AbstractPagedRequestHandler<GetInvoicesQuery, InvoiceDto>
    {
        private readonly Context _context;

        public GetInvoicesQueryHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result<Page<InvoiceDto>>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {
            int count = await _context.Invoices.CountAsync(m => m.ShopId == request.ShopId, cancellationToken);
            
            List<InvoiceDto> invoices = await _context.Invoices
                .Include(m => m.InvoiceItems)
                .Where(m => m.ShopId == request.ShopId)
                .ApplyLimit(request)
                .Select(m => new InvoiceDto
                {
                    Id = m.Id,
                    Status = m.Status,
                    InvoiceItems = m.InvoiceItems.Select(r => new InvoiceItemDto
                    {
                        Id = r.Id,
                        Quantity = r.Quantity,
                        Price = r.Price,
                        Description = r.Description,
                        Creation = r.Creation
                    }),
                    Creation = m.Creation
                }).AsNoTracking().ToListAsync(cancellationToken);

            return Ok(invoices, count);
        }
    }
}