using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Client.Application.Queries.Commands.GetCommands.Models;
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

namespace GoLocal.Client.Application.Queries.Commands.GetCommands
{
    public class GetCommandsQueryHandler : AbstractPagedRequestHandler<GetCommandsQuery, CommandDto>
    {
        private readonly Context _context;
        private readonly IUserAccessor<User> _user;

        public GetCommandsQueryHandler(Context context, IUserAccessor<User> user)
        {
            _context = context;
            _user = user;
        }

        public override async Task<Result<Page<CommandDto>>> Handle(GetCommandsQuery request, CancellationToken cancellationToken)
        {
            User user = await _user.GetUserAsync();
            int count = await _context.Commands.CountAsync(m => m.UserId == user.Id, cancellationToken);
            
            List<Command> commands = await _context.Commands
                .Where(m => m.UserId == user.Id)
                .ApplyLimit(request)
                .ToListAsync(cancellationToken);
            
            return Ok(commands.Adapt<List<CommandDto>>(), count);      
        }
    }
}