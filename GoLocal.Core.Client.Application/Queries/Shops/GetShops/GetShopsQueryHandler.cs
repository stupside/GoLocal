using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Bus.Results.Pages;
using GoLocal.Core.Client.Application.Queries.Shops.GetShops.Models;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Domain.Enums;
using GoLocal.Shared.Locate.Interfaces;
using GoLocal.Shared.Locate.Models.Search;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Context = GoLocal.Core.Persistence.EntityFramework.Context;

namespace GoLocal.Core.Client.Application.Queries.Shops.GetShops
{
    public class GetShopsQueryHandler : AbstractPagedRequestHandler<GetShopsQuery, ShopDto>
    {
        private readonly IUserAccessor<User> _accessor;
        private readonly ILocateService _locate;
        private readonly Context _context;

        public GetShopsQueryHandler(Context context, ILocateService locate, IUserAccessor<User> accessor)
        {
            _context = context;
            _locate = locate;
            _accessor = accessor;
        }

        public override async Task<Result<Page<ShopDto>>> Handle(GetShopsQuery request, CancellationToken cancellationToken)
        {
            Place place = await _locate.GetPosition(request.Location);
            if (place is not {Any: true})
                return BadRequest("Something went wrong when we tried to localize your shop");
            
            Feature feature = place.Feature;
            
            User user = await _accessor.GetUserAsync();
            var logged = user != null;
            
            int count = await _context.Shops.Where(m => 
                    Math.Acos(
                        Math.Sin(m.Location.Latitude) * Math.Sin(feature.Latitude) + 
                        Math.Cos(m.Location.Latitude)*Math.Cos(feature.Latitude)*Math.Cos(feature.Longitude-m.Location.Longitude)
                        ) <= request.Range && (m.Visibility == Visibility.Public || logged && m.UserId == user.Id))
                .CountAsync(cancellationToken);
            
            _ = TypeAdapterConfig<Shop, ShopDto>.NewConfig()
                .Map(dest => dest.Image, src => src.Image == null ? null : Convert.ToBase64String(src.Image));

            List<ShopDto> shops = await _context.Shops
                .Where(m =>
                    (m.Name.Contains(request.Name) || m.Services.Any(r => r.Name.Contains(request.Name)) || m.Services.Any(r => r.Name.Contains(request.Name))) &&
                    Math.Acos(
                        Math.Sin(m.Location.Latitude) * Math.Sin(feature.Latitude) + 
                        Math.Cos(m.Location.Latitude)*Math.Cos(feature.Latitude)*Math.Cos(feature.Longitude-m.Location.Longitude)
                    ) <= request.Range && (m.Visibility == Visibility.Public || logged && m.UserId == user.Id))
                .Include(m => m.Openings)
                .Include(m => m.User)
                .ProjectToType<ShopDto>()
                .ToListAsync(cancellationToken);

            return Ok(shops, count);        
        }
    }
}