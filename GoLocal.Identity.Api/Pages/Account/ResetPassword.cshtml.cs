using System.Threading.Tasks;
using GoLocal.Identity.Application.Commands.Users.ResetPassword;
using GoLocal.Shared.Bus.Results.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoLocal.Identity.Api.Pages.Account
{
    public class ResetPassword : PageModel
    {
        private readonly IMediator _mediator;

        public ResetPassword(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void OnGet() {}

        [BindProperty]
        public ResetPasswordCommand Command { get; set; }
        
        public async Task<IActionResult> OnPost()
        {
            var result = await _mediator.Send(Command.SetCallback(Url.PageLink(nameof(ResetPasswordConfirmation))));
            if (result.Status != ResultStatus.Ok)
                return BadRequest(result.Message);

            return RedirectToPage();
        }
    }
}