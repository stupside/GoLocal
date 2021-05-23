using System.Threading;
using System.Threading.Tasks;
using GoLocal.Domain.Entities;
using GoLocal.Domain.Enums;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Artisan.Application.Commands.Commands.AcceptCommand
{
    public class AcceptCommandCommandHandler : AbstractRequestHandler<AcceptCommandCommand>
    {
        private readonly Context _context;

        public AcceptCommandCommandHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result> Handle(AcceptCommandCommand request, CancellationToken cancellationToken)
        {
            Command command = await _context.Commands.SingleOrDefaultAsync(m => m.Id == request.CommandId, cancellationToken);

            if (command == null)
                return NotFound<Command>(request.CommandId);

            if (command.Status != CommandStatus.Pending)
                return BadRequest($"You can't accept or reject this command. The status of the command was '{command.Status}'");

            command.Status = request.Accept ? CommandStatus.Accepted : CommandStatus.Rejected;
            
            _context.Commands.Update(command);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}