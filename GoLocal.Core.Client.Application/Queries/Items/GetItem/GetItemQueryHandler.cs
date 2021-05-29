using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
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
            _ = TypeAdapterConfig<Item, GetItemResponse>.NewConfig()
                .Map(dest => dest.Image, src => src.Image == null ? null : Convert.ToBase64String(src.Image));

            User user = await _accessor.GetUserAsync();
            
            GetItemResponse item = await _context.Items
                .Include(m => m.Packages)
                .Include(m => m.Comments)
                .Where(m => m.Id == request.ItemId && m.ShopId == request.ShopId && 
                            m.Visibility != Visibility.Deleted && (m.Visibility != Visibility.Private || m.Shop.UserId == user.Id))
                .ProjectToType<GetItemResponse>()
                .SingleOrDefaultAsync(cancellationToken);

            if (item == null)
                return NotFound<Item>(request.ItemId);

            return Ok(item);
        }
    }
}