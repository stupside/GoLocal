using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Domain.Enums;
using GoLocal.Core.Persistence.EntityFramework;
using Mapster;
using Microsoft.EntityFrameworkCore;

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
            
            Shop shop = await _context.Shops
                .Include(m => m.Services)
                .Include(m => m.Products)
                .Include(m => m.Openings)
                .Include(m => m.User)
                .SingleOrDefaultAsync(m => m.Id == request.ShopId && 
                                           (m.Visibility == Visibility.Public || logged && m.UserId == user.Id), cancellationToken);

            if (shop == null)
                return NotFound<Shop>(request.ShopId);

            var rate = (await _context.Comments.Where(m => m.Item.ShopId == request.ShopId).Select(m => m.Rate)
                    .ToListAsync(cancellationToken)).DefaultIfEmpty().Average();

            _ = TypeAdapterConfig<Shop, GetShopResponse>.NewConfig()
                .Map(dest => dest.Image, src => src.Image == null ? null : Convert.ToBase64String(src.Image))
                .Map(dest => dest.Rate, src => rate);

            return Ok(shop.Adapt<GetShopResponse>());
        }
    }
}