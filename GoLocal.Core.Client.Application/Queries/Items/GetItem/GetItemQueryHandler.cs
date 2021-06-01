using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Client.Application.Queries.Items.GetItem.Models;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Entities.Abstracts;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Domain.Enums;
using GoLocal.Core.Persistence.EntityFramework;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Client.Application.Queries.Items.GetItem
{
    public class GetItemQueryHandler : AbstractRequestHandler<GetItemQuery, GetItemResponse>
    {
        private readonly IUserAccessor<User> _accessor;
        private readonly Context _context;

        public GetItemQueryHandler(Context context, IUserAccessor<User> accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public override async Task<Result<GetItemResponse>> Handle(GetItemQuery request, CancellationToken cancellationToken)
        {
            // TODO: Since Item->Shop throw unmapped we have to query it first to check the status
            var shop = await _context.Shops
                .Where(m => m.Id == request.ShopId)
                .Select(m => new {m.Visibility , m.UserId}).SingleOrDefaultAsync(cancellationToken);
            
            if (shop == null)
                return NotFound<Product>(request.ShopId);
            
            User user = await _accessor.GetUserAsync();
            var logged = user != null;
            var owner = logged && shop.UserId == user.Id; // TODO: Fix Item->Shop navigation
            
            if(shop.Visibility == Visibility.Private && !owner)
                return NotFound<Product>(request.ShopId);

            GetItemResponse item = await _context.Items
                .Include(m => m.Comments)
                .Include(m => m.Packages.Where(r => r.Visibility == Visibility.Public || owner))
                .Where(m => m.Id == request.ItemId && m.ShopId == request.ShopId &&
                            (m.Visibility == Visibility.Public || owner))
                .Select(m => new GetItemResponse
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    Creation = m.Creation,
                    Comments = m.Comments.Select(r => new CommentDto
                    {
                        Id = r.Id,
                        Rate = r.Rate,
                        Body = r.Body,
                        Creation = r.Creation
                    }),
                    Packages = m.Packages.Select(r => new PackageDto
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Description = r.Description,
                        Stocks = r.Stocks,
                        Price = r.Price
                    }),
                    Image = m.Image == null ? null : Convert.ToBase64String(m.Image)
                }).SingleOrDefaultAsync(cancellationToken);

            if (item == null)
                return NotFound<Item>(request.ItemId);

            return Ok(item);
        }
    }
}