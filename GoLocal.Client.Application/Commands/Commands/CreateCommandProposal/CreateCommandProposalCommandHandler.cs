using System.Threading;
using System.Threading.Tasks;
using GoLocal.Domain.Entities;
using GoLocal.Domain.Entities.Identity;
using GoLocal.Domain.Enums;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Accessor.Accessors;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Client.Application.Commands.Commands.CreateCommandProposal
{
    public class CreateCommandProposalCommandHandler : AbstractRequestHandler<CreateCommandProposalCommand, int>
    {
        private readonly IUserAccessor<User> _user;
        private readonly Context _context;

        public CreateCommandProposalCommandHandler(Context context, IUserAccessor<User> user)
        {
            _context = context;
            _user = user;
        }

        public override async Task<Result<int>> Handle(CreateCommandProposalCommand request, CancellationToken cancellationToken)
        {
            Command command = await _context.Commands.SingleOrDefaultAsync(m => m.Id == request.CommandId, cancellationToken);

            if (command == null)
                return NotFound<Command>(request.CommandId);
            
            if (command.Status != CommandStatus.Accepted)
                return BadRequest($"The status of the command was {command.Status}");
            
            if (await _context.CommandProposals.AnyAsync(m => m.Approved && m.CommandId == request.CommandId,
                cancellationToken))
                return BadRequest("You can't change the specifications, One proposal have been validated");

            User user = await _user.GetUserAsync();

            CommandProposal proposal = new CommandProposal(command, user, request.Price, request.Specification);

            await _context.CommandProposals.AddAsync(proposal, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Ok(proposal.Id);
        }
    }
}