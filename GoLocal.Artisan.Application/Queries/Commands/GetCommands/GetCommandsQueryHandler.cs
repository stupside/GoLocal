using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Artisan.Application.Queries.Commands.GetCommands.Models;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Bus.Commons.Filtering;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using GoLocal.Shared.Bus.Results.Pages;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Artisan.Application.Queries.Commands.GetCommands
{
    public class GetCommandsQueryHandler : AbstractPagedRequestHandler<GetCommandsQuery, CommandDto>
    {
        private readonly Context _context;

        public GetCommandsQueryHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result<Page<CommandDto>>> Handle(GetCommandsQuery request, CancellationToken cancellationToken)
        {
            var count = await _context.Commands.CountAsync(m => m.Package.Item.ShopId == request.ShopId, cancellationToken);
            var commands = await _context.Commands
                .Where(m => m.Package.Item.ShopId == request.ShopId)
                .Include(m => m.Package)
                .ApplyLimit(request)
                .ToListAsync(cancellationToken);

            return Ok(commands.Adapt<List<CommandDto>>(), count);
        }
    }
}