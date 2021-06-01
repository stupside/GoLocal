using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Bus.Results.Pages;
using GoLocal.Core.Client.Application.Queries.Carts.GetCarts.Models;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Client.Application.Queries.Carts.GetCarts
{
    public class GetCartsQueryHandler :  AbstractPagedRequestHandler<GetCartsQuery, CartDto>
    {
        private readonly IUserAccessor<User> _accessor;
        private readonly Context _context;

        public GetCartsQueryHandler(Context context, IUserAccessor<User> accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public override async Task<Result<Page<CartDto>>> Handle(GetCartsQuery request, CancellationToken cancellationToken)
        {
            User user = await _accessor.GetUserAsync();
            
            int count = await _context.Carts.CountAsync(m => m.UserId == user.Id, cancellationToken);

            List<CartDto> carts = await _context.Carts
                .Include(m => m.Shop)
                .Include(m => m.CartPackages).ThenInclude(m => m.Package).ThenInclude(m => m.Item)
                .Where(m => m.UserId == user.Id)
                .Select(m => new CartDto
                {
                    Shop = new ShopDto
                    {
                        Id = m.Shop.Id,
                        Name = m.Shop.Name,
                        Visibility = m.Shop.Visibility
                    },
                    CartPackages = m.CartPackages.Select(r => new CartPackageDto
                    {
                        Quantity = r.Quantity,
                        Price = r.Price,
                        Package = new PackageDto
                        {
                            Id = r.Package.Id,
                            Name = r.Package.Name,
                            Description = r.Package.Description,
                            Stocks = r.Package.Stocks,
                            Price = r.Package.Price,
                            Visibility = r.Package.Visibility,
                            Item = new ItemDto
                            {
                                Id = r.Package.Item.Id,
                                Name = r.Package.Item.Name,
                                Visibility =  r.Package.Item.Visibility
                            }
                        }
                    }),
                    Creation = m.Creation
                }).AsNoTracking().ToListAsync(cancellationToken);

            return Ok(carts, count);
        }
    }
}