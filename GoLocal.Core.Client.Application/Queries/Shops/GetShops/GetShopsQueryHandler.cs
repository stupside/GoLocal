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
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Domain.Enums;
using GoLocal.Shared.Locate.Interfaces;
using GoLocal.Shared.Locate.Models.Search;
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

            List<ShopDto> shops = await _context.Shops
                .Include(m => m.User)
                .Where(m =>
                    (m.Name.Contains(request.Name) || m.Services.Any(r => r.Name.Contains(request.Name)) || m.Services.Any(r => r.Name.Contains(request.Name))) &&
                    Math.Acos(
                        Math.Sin(m.Location.Latitude) * Math.Sin(feature.Latitude) + 
                        Math.Cos(m.Location.Latitude)*Math.Cos(feature.Latitude)*Math.Cos(feature.Longitude-m.Location.Longitude)
                    ) <= request.Range && (m.Visibility == Visibility.Public || logged && m.UserId == user.Id))
                .Select(m => new ShopDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    User = new UserDto
                    {
                        Id = m.User.Id,
                        UserName = m.User.UserName
                    },
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
                    Creation = m.Creation,
                    Image = m.Image == null ? null : Convert.ToBase64String(m.Image)
                }).AsNoTracking().ToListAsync(cancellationToken);

            return Ok(shops, count);        
        }
    }
}