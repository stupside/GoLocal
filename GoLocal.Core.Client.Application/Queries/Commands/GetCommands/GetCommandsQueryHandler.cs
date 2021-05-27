using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Bus.Commons.Filtering;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Bus.Results.Pages;
using GoLocal.Core.Client.Application.Queries.Commands.GetCommands.Models;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Persistence.EntityFramework;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Client.Application.Queries.Commands.GetCommands
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