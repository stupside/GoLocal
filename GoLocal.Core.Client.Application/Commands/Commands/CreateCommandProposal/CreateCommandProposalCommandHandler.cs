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

namespace GoLocal.Core.Client.Application.Commands.Commands.CreateCommandProposal
{
    public class CreateCommandProposalCommandHandler : AbstractRequestHandler<CreateCommandProposalCommand, int>
    {
        private readonly IUserAccessor<User> _accessor;
        private readonly Context _context;

        public CreateCommandProposalCommandHandler(Context context, IUserAccessor<User> accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public override async Task<Result<int>> Handle(CreateCommandProposalCommand request, CancellationToken cancellationToken)
        {
            Command command = await _context.Commands.SingleOrDefaultAsync(m => m.Id == request.CommandId, cancellationToken);
            if (command == null)
                return NotFound<Command>(request.CommandId);
            
            User user = await _accessor.GetUserAsync(); // TODO: IMPROVE
            string uid = await _context.Shops.Where(m => m.Id == command.ShopId)
                .Select(m => m.UserId).SingleOrDefaultAsync(cancellationToken);
            if (command.UserId != user.Id || command.UserId != uid)
                return Unauthorized();
            
            if (command.Status != CommandStatus.Accepted)
                return BadRequest($"The status of the command was {command.Status}");
            
            if (await _context.CommandProposals.AnyAsync(m => m.Approved && m.CommandId == request.CommandId,
                cancellationToken))
                return BadRequest("You can't change the specifications, One proposal have been validated");

            CommandProposal proposal = new CommandProposal(command, user, request.Price, request.Specification);

            await _context.CommandProposals.AddAsync(proposal, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Ok(proposal.Id);
        }
    }
}