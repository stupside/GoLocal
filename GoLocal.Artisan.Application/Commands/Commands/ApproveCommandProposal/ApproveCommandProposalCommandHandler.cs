using System.Threading;
using System.Threading.Tasks;
using GoLocal.Domain.Entities;
using GoLocal.Domain.Entities.Identity;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Accessor.Accessors;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Artisan.Application.Commands.Commands.ApproveCommandProposal
{
    public class ApproveCommandProposalCommandHandler : AbstractRequestHandler<ApproveCommandProposalCommand>
    {
        private readonly IUserAccessor<User> _user;
        private readonly Context _context;

        public ApproveCommandProposalCommandHandler(IUserAccessor<User> user, Context context)
        {
            _user = user;
            _context = context;
        }

        public override async Task<Result> Handle(ApproveCommandProposalCommand request, CancellationToken cancellationToken)
        {
            if (await _context.CommandProposals.AnyAsync(m => m.Accepted && m.CommandId == request.CommandId,
                cancellationToken))
                return BadRequest("You can't change the specifications, One proposal have been validated");
            
            CommandProposal proposal = await _context.CommandProposals.SingleOrDefaultAsync(
                m => m.Id == request.CommandProposalId && m.CommandId == request.CommandId, cancellationToken);

            if (proposal == null)
                return NotFound<CommandProposal>(request.CommandProposalId);

            if (proposal.Accepted)
                return BadRequest("This specification is already accepted");

            User user = await _user.GetUserAsync();

            if (proposal.UserId == user.Id)
                return BadRequest("You can't approve your own proposal");

            proposal.Accepted = true;
            
            _context.Update(proposal);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}