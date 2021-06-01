using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Commons.Filtering;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Bus.Results.Pages;
using GoLocal.Core.Artisan.Application.Queries.Commands.GetCommands.Models;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Artisan.Application.Queries.Commands.GetCommands
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
            
            List<CommandDto> commands = await _context.Commands
                .Where(m => m.Package.Item.ShopId == request.ShopId)
                .Include(m => m.User)
                .Include(m => m.Package).ThenInclude(m => m.Item)
                .Include(m => m.CommandProposals.Where(r => r.Approved))
                .ApplyLimit(request)
                .Select(m => new CommandDto
                {
                    Id = m.Id,
                    Creation = m.Creation,
                    Status = m.Status,
                    Price = m.CommandProposals.FirstOrDefault().Price,
                    Specification = m.CommandProposals.FirstOrDefault().Specification,
                    Item = new ItemDto
                    {
                        Id = m.Package.Item.Id,
                        Name = m.Package.Item.Name,
                        Package = new PackageDto
                        {
                            Id = m.Package.Id,
                            Name = m.Package.Name,
                        },
                    },
                    User = new UserDto
                    {
                        Id = m.User.Id,
                        UserName = m.User.UserName
                    }
                }).AsNoTracking().ToListAsync(cancellationToken);

            return Ok(commands, count);
        }
    }
}