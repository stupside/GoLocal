using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Client.Application.Queries.Carts.GetCarts.Models;
using GoLocal.Core.Client.Application.Queries.Shops.GetShop.Models;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Domain.Enums;
using GoLocal.Core.Persistence.EntityFramework;
using Mapster;
using Microsoft.EntityFrameworkCore;
using ItemDto = GoLocal.Core.Client.Application.Queries.Shops.GetShop.Models.ItemDto;

namespace GoLocal.Core.Client.Application.Queries.Shops.GetShop
{
    public class GetShopQueryHandler : AbstractRequestHandler<GetShopQuery, GetShopResponse>
    {
        private readonly IUserAccessor<User> _accessor;
        private readonly Context _context;

        public GetShopQueryHandler(Context context, IUserAccessor<User> accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public override async Task<Result<GetShopResponse>> Handle(GetShopQuery request, CancellationToken cancellationToken)
        {
            User user = await _accessor.GetUserAsync();
            var logged = user != null;
            
            var rate = (await _context.Comments.Where(m => m.Item.ShopId == request.ShopId).Select(m => m.Rate)
                .ToListAsync(cancellationToken)).DefaultIfEmpty().Average();
            
            GetShopResponse shop = await _context.Shops
                .Include(m => m.Services)
                .Include(m => m.Products)
                .Include(m => m.Openings)
                .Include(m => m.User)
                .Where(m => m.Id == request.ShopId && (m.Visibility == Visibility.Public || logged && m.UserId == user.Id))
                .Select(m => new GetShopResponse
                {
                    Id = m.Id,
                    Name = m.Name,
                    Rate = rate,
                    Location = new LocationDto
                    {
                        PostCode = m.Location.PostCode,
                        Country = m.Location.Country,
                        Region = m.Location.Region,
                        City = m.Location.City,
                        NeighborHood = m.Location.NeighborHood,
                        Street = m.Location.Street,
                        Address = m.Location.Address,
                        Longitude = m.Location.Longitude,
                        Latitude = m.Location.Latitude
                    },
                    Contact = new ContactDto
                    {
                        Phone = m.Contact.Phone,
                        Email = m.Contact.Email
                    },
                    Products = m.Products.Select(r => new ItemDto
                    {
                        Id = r.Id,
                        Image = r.Image == null ? null : Convert.ToBase64String(r.Image),
                        Name = r.Name,
                        Description = r.Description,
                        Creation = r.Creation
                    }),
                    Services = m.Services.Select(r => new ItemDto
                    {
                        Id = r.Id,
                        Image = r.Image == null ? null : Convert.ToBase64String(r.Image),
                        Name = r.Name,
                        Description = r.Description,
                        Creation = r.Creation
                    }),
                    Openings = m.Openings.Select(r => new OpeningDto
                    {
                        Day = r.Day,
                        Morning = r.Morning,
                        Evening = r.Evening
                    }),
                    User = new UserDto
                    {
                        Id = m.User.Id,
                        UserName = m.User.UserName
                    },
                    Creation = m.Creation,
                    Image = m.Image == null ? null : Convert.ToBase64String(m.Image)
                }).SingleOrDefaultAsync(cancellationToken);

            if (shop == null)
                return NotFound<Shop>(request.ShopId);
            
            return Ok(shop);
        }
    }
}