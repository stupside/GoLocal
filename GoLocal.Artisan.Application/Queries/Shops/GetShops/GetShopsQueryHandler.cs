using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Artisan.Application.Queries.Shops.GetShops.Models;
using GoLocal.Domain.Entities;
using GoLocal.Domain.Entities.Identity;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Accessor.Accessors;
using GoLocal.Shared.Bus.Commons.Filtering;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using GoLocal.Shared.Bus.Results.Pages;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Artisan.Application.Queries.Shops.GetShops
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

            IEnumerable<Shop> shops = await _context.Shops
                .Where(m => m.UserId == user.Id)
                .ApplyLimit(request)
                .ToListAsync(cancellationToken);

            return Ok(shops.Adapt<List<ShopDto>>(), count);
        }
    }
}