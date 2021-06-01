using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Bus.Commons.Filtering;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Bus.Results.Pages;
using GoLocal.Core.Artisan.Application.Queries.Shops.GetShops.Models;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Persistence.EntityFramework;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Artisan.Application.Queries.Shops.GetShops
{
    public class GetShopsQueryHandler : AbstractPagedRequestHandler<GetShopsQuery, ShopDto>
    {
        private readonly Context _context;
        private readonly IUserAccessor<User> _user;

        public GetShopsQueryHandler(IUserAccessor<User> user, Context context)
        {
            _user = user;
            _context = context;
        }

        public override async Task<Result<Page<ShopDto>>> Handle(GetShopsQuery request, CancellationToken cancellationToken)
        {
            User user = await _user.GetUserAsync();
            
            int count = await _context.Shops
                .CountAsync(m => m.UserId == user.Id, cancellationToken);

            _ = TypeAdapterConfig<Shop, ShopDto>.NewConfig()
                .Map(dest => dest.Image, src => src.Image == null ? null : Convert.ToBase64String(src.Image));
            
            List<ShopDto> shops = await _context.Shops
                .Where(m => m.UserId == user.Id)
                .ApplyLimit(request)
                .Select(m => new ShopDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Visibility = m.Visibility,
                    Image = m.Image == null ? null : Convert.ToBase64String(m.Image),
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
                    Creation = m.Creation
                }).AsNoTracking().ToListAsync(cancellationToken);

            return Ok(shops, count);
        }
    }
}