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
using Mapster;
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
                .Include(m => m.CartPackages).ThenInclude(m => m.Package)
                .Where(m => m.UserId == user.Id)
                .ProjectToType<CartDto>()
                .ToListAsync(cancellationToken);

            return Ok(carts, count);
        }
    }
}