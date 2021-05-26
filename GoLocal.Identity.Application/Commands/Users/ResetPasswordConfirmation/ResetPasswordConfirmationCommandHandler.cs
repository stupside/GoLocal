using System.Net;
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
            User user = await _user.FindByIdAsync(request.Uid);
            if (user == null)
                return Ok();

            var result = await _user.ResetPasswordAsync(user, WebUtility.UrlDecode(request.Token), request.Password);

            return result.Succeeded ? Ok() : BadRequest("Password reset failed");
        }
    }
}