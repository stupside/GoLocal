using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Client.Application.Queries.Invoices.GetInvoice.Models;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Client.Application.Queries.Invoices.GetInvoice
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
                .Include(m => m.Shop)
                .Include(m => m.InvoiceItems).ThenInclude(m => m.Package).ThenInclude(m => m.Item)
                .Where(m => m.Id == request.InvoiceId)
                .Select(m => new GetInvoiceResponse{
                    Id = m.Id,
                    Status = m.Status,
                    Shop = new ShopDto
                    {
                        Id = m.Shop.Id,
                        Name = m.Shop.Name
                    },
                    InvoiceItems = m.InvoiceItems.Select(r => new InvoiceItemDto
                    {
                        Id = r.Id,
                        Quantity = r.Quantity,
                        Price = r.Price,
                        Description = r.Description,
                        Creation = r.Creation,
                        Item = new ItemDto
                        {
                            Id = r.Package.Item.Id,
                            Name = r.Package.Item.Name,
                            Package = new PackageDto
                            {
                                Id = r.Package.Id,
                                Name = r.Package.Name
                            }
                        }
                    }),
                    Creation = m.Creation
                }).AsNoTracking().SingleOrDefaultAsync(cancellationToken);

            if (invoice == null)
                return NotFound<Invoice>(request.InvoiceId);

            return Ok(invoice);
        }
    }
}