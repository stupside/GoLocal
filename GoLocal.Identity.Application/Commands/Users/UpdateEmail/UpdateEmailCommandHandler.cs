using System.Threading;
using System.Threading.Tasks;
using GoLocal.Identity.Application.Commons.Accessor;
using GoLocal.Identity.Domain.Entities;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using MediatR;

namespace GoLocal.Identity.Application.Commands.Users.UpdateEmail
{
    public class UpdateEmailCommandHandler : AbstractRequestHandler<UpdateEmailCommand>
    {
        private readonly IUserAccessor _user;

        public UpdateEmailCommandHandler(IUserAccessor user)
        {
            _user = user;
        }

        public override async Task<Result<Unit>> Handle(UpdateEmailCommand request, CancellationToken cancellationToken)
        {
            User user = await _user.GetUserAsync();

            string token = await _user.Manager.GenerateChangeEmailTokenAsync(user, request.Email);
            // TODO: Send a mail to the user with the token.
            await _user.Manager.ChangeEmailAsync(user, request.Email, token);

            return Ok();
        }
    }
}