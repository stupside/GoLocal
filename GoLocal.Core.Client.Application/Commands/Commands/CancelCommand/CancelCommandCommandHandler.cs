using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Domain.Enums;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Client.Application.Commands.Commands.CancelCommand
{
    public class CancelCommandCommandHandler : AbstractRequestHandler<CancelCommandCommand>
    {
        private readonly IUserAccessor<User> _accessor;
        private readonly Context _context;

        public CancelCommandCommandHandler(Context context, IUserAccessor<User> accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public override async Task<Result> Handle(CancelCommandCommand request, CancellationToken cancellationToken)
        {
            Command command = await _context.Commands
                .SingleOrDefaultAsync(m => m.Id == request.CommandId, cancellationToken);

            if (command == null)
                return NotFound<Command>(request.CommandId);

            if (command.Status is not CommandStatus.Accepted or CommandStatus.Pending)
                return BadRequest($"You can't cancel this command. The status of the command was '{command.Status}'");
            
            User user = await _accessor.GetUserAsync();
            string uid = await _context.Shops
                .Where(m => m.Id == command.ShopId)
                .Select(m => m.UserId).SingleOrDefaultAsync(cancellationToken);
            if (command.UserId != user.Id || command.UserId != uid)
                return Unauthorized();

            command.Status = CommandStatus.Canceled;
            
            _context.Commands.Update(command);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}