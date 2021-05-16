using System.Threading;
using System.Threading.Tasks;
using GoLocal.Identity.Domain.Entities;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using Microsoft.AspNetCore.Identity;

namespace GoLocal.Identity.Application.Commands.Users.ResetPasswordConfirmation
{
    public class ResetPasswordConfirmationCommandHandler : AbstractRequestHandler<ResetPasswordConfirmationCommand>
    {
        private readonly UserManager<User> _user;

        public ResetPasswordConfirmationCommandHandler(UserManager<User> user)
        {
            _user = user;
        }

        public override async Task<Result> Handle(ResetPasswordConfirmationCommand request, CancellationToken cancellationToken)
        {
            User user = await _user.FindByEmailAsync(request.Email);
            if (user == null)
                return Ok();

            if (await _user.CheckPasswordAsync(user, request.OldPassword))
                return Ok();

            var result = await _user.ResetPasswordAsync(user, request.Token, request.NewPassword);

            return !result.Succeeded ? BadRequest("Password reset failed") : Ok();
        }
    }
}