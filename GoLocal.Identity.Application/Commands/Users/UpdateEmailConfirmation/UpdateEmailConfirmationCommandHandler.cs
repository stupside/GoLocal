using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using GoLocal.Identity.Domain.Entities;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using Microsoft.AspNetCore.Identity;

namespace GoLocal.Identity.Application.Commands.Users.UpdateEmailConfirmation
{
    public class UpdateEmailConfirmationCommandHandler : AbstractRequestHandler<UpdateEmailConfirmationCommand>
    {
        private readonly UserManager<User> _user;

        public UpdateEmailConfirmationCommandHandler(UserManager<User> user)
        {
            _user = user;
        }
        
        public override async Task<Result> Handle(UpdateEmailConfirmationCommand request, CancellationToken cancellationToken)
        {
            User user = await _user.FindByIdAsync(request.Uid);
            if (user == null)
                return Ok();
            
            var result = await _user.ChangeEmailAsync(user, request.Email, HttpUtility.UrlDecode(request.Token));

            return result.Succeeded ? Ok() : BadRequest("Email confirmation failed");
        }
    }
}