using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Client.Application.Queries.Shops.GetShops.Models;
using GoLocal.Domain.Entities;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Bus.Commons.Filtering;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using GoLocal.Shared.Bus.Results.Pages;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Client.Application.Queries.Shops.GetShops
{
    public class GetShopsQueryHandler : AbstractPagedRequestHandler<GetShopsQuery, ShopDto>
    {
        private readonly Context _context;

        public GetShopsQueryHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result<Page<ShopDto>>> Handle(GetShopsQuery request, CancellationToken cancellationToken)
        {
            int count = await _context.Shops.CountAsync(cancellationToken);

            IEnumerable<Shop> shops = await _context.Shops
                .Include(m => m.Openings)
                .Include(m => m.User)
                .ApplySearch(request)
                .ApplyLimit(request)
                .ToListAsync(cancellationToken);

            return Ok(shops.Adapt<List<ShopDto>>(), count);        
        }
    }
}