using System.Threading;
using System.Threading.Tasks;
using System.Web;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Identity.Domain.Entities;
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

            var result = await _user.ResetPasswordAsync(user, HttpUtility.UrlDecode(request.Token), request.Password);

            return result.Succeeded ? Ok() : BadRequest("Password reset failed");
        }
    }
}