using System.Threading;
using System.Threading.Tasks;
using GoLocal.Identity.Application.Commons.Accessor;
using GoLocal.Identity.Domain.Entities;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using MediatR;

namespace GoLocal.Identity.Application.Commands.Users.UpdateEmailConfirmation
{
    public class UpdateEmailConfirmationCommandHandler : AbstractRequestHandler<UpdateEmailConfirmationCommand>
    {
        private readonly IUserAccessor _user;

        public UpdateEmailConfirmationCommandHandler(IUserAccessor user)
        {
            _user = user;
        }
        
        public override async Task<Result<Unit>> Handle(UpdateEmailConfirmationCommand request, CancellationToken cancellationToken)
        {
            User user = await _user.GetUserAsync();
            var result = await _user.Manager.ChangeEmailAsync(user, request.Email, request.Token);
            
            return !result.Succeeded ? BadRequest("Email confirmation failed") : Ok();
        }
    }
}