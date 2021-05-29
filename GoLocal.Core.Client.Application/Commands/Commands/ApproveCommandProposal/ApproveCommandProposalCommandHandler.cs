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

namespace GoLocal.Core.Client.Application.Commands.Commands.ApproveCommandProposal
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
            CommandProposal proposal = await _context.CommandProposals
                .Include(m => m.Command)
                .SingleOrDefaultAsync(m => m.Id == request.CommandProposalId && m.CommandId == request.CommandId, cancellationToken);
            
            if (proposal == null)
                return NotFound<CommandProposal>(request.CommandProposalId);
            
            if (proposal.Command.Status is not CommandStatus.Accepted)
                return BadRequest($"You can't cancel this command. The status of the command was '{proposal.Command.Status}'");

            User user = await _user.GetUserAsync();
            string uid = await _context.Shops.Where(m => m.Id == proposal.Command.ShopId).Select(m => m.UserId).SingleOrDefaultAsync(cancellationToken);
            if (proposal.Command.UserId != user.Id || proposal.Command.UserId != uid)
                return Unauthorized();
            
            if (await _context.CommandProposals.AnyAsync(m => m.Approved && m.CommandId == request.CommandId,
                cancellationToken))
                return BadRequest("You can't change the specifications, One proposal have been validated");

            if (proposal.Approved)
                return BadRequest("This specification is already approved");

            if (proposal.UserId == user.Id)
                return BadRequest("You can't approve your own proposal");

            proposal.Approved = true;
            
            _context.Update(proposal);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}