using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Client.Application.Queries.Invoices.GetInvoice.Models;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Domain.Enums;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Client.Application.Queries.Invoices.GetInvoice
{
    public class GetInvoiceQueryHandler : AbstractRequestHandler<GetInvoiceQuery, GetInvoiceResponse>
    {
        private readonly IUserAccessor<User> _accessor;
        private readonly Context _context;

        public GetInvoiceQueryHandler(Context context, IUserAccessor<User> accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public override async Task<Result<GetInvoiceResponse>> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            User user = await _accessor.GetUserAsync();

            GetInvoiceResponse invoice = await _context.Invoices
                .Include(m => m.Shop)
                .Include(m => m.InvoiceItems)
                .ThenInclude(m => m.Package)
                .ThenInclude(m => m.Item)
                .Where(m => m.Id == request.InvoiceId && m.UserId == user.Id)
                .Select(m => new GetInvoiceResponse(){
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
                }).SingleOrDefaultAsync(cancellationToken);

            if (invoice == null)
                return NotFound<Invoice>(request.InvoiceId);
            
            
            
            throw new System.NotImplementedException();
        }
    }
}