using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Enums;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Client.Application.Commands.Commands.CancelCommand
{
    public class CancelCommandCommandHandler : AbstractRequestHandler<CancelCommandCommand>
    {
        private readonly Context _context;

        public CancelCommandCommandHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result> Handle(CancelCommandCommand request, CancellationToken cancellationToken)
        {
            Command command = await _context.Commands
                .SingleOrDefaultAsync(m => m.Id == request.CommandId, cancellationToken);

            if (command == null)
                return NotFound<Command>(request.CommandId);

            if (command.Status == CommandStatus.Rejected || 
                command.Status == CommandStatus.Accepted ||
                command.Status == CommandStatus.Canceled)
                return BadRequest($"You can't cancel this command. The status of the command was '{command.Status}'");

            command.Status = CommandStatus.Canceled;
            
            _context.Commands.Update(command);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}