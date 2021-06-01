using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Bus.Commons.Filtering;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Bus.Results.Pages;
using GoLocal.Core.Client.Application.Queries.Invoices.GetInvoices.Models;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Client.Application.Queries.Invoices.GetInvoices
{
    public class GetInvoicesQueryHandler : AbstractPagedRequestHandler<GetInvoicesQuery, InvoiceDto>
    {
        private readonly Context _context;
        private readonly IUserAccessor<User> _accessor;

        public GetInvoicesQueryHandler(IUserAccessor<User> accessor, Context context)
        {
            _accessor = accessor;
            _context = context;
        }

        public override async Task<Result<Page<InvoiceDto>>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {
            User user = await _accessor.GetUserAsync();

            int count = await _context.Invoices.CountAsync(m => m.UserId == user.Id, cancellationToken);
            
            List<InvoiceDto> invoices = await _context.Invoices
                .Include(m => m.Shop)
                .Include(m => m.InvoiceItems)
                .Where(m => m.UserId == user.Id)
                .ApplyLimit(request)
                .Select(m => new InvoiceDto
                {
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
                        Creation = r.Creation
                    }),
                    Creation = m.Creation
                }).AsNoTracking().ToListAsync(cancellationToken);

            return Ok(invoices, count);
        }
    }
}