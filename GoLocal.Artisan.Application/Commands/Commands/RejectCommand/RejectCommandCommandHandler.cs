using System.Threading;
using System.Threading.Tasks;
using GoLocal.Domain.Entities;
using GoLocal.Domain.Enums;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Artisan.Application.Commands.Commands.RejectCommand
{
    public class RejectCommandCommandHandler : AbstractRequestHandler<RejectCommandCommand>
    {
        private readonly Context _context;

        public RejectCommandCommandHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result> Handle(RejectCommandCommand request, CancellationToken cancellationToken)
        {
            Command command = await _context.Commands.SingleOrDefaultAsync(m => m.Id == request.CommandId, cancellationToken);

            if (command == null)
                return NotFound<Command>(request.CommandId);

            if (command.Status == CommandStatus.Rejected ||
                command.Status == CommandStatus.Canceled)
                return BadRequest($"You can't cancel this command. The status of the command was '{command.Status}'");

            command.Status = CommandStatus.Rejected;
            
            _context.Commands.Update(command);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}