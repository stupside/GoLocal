using System.Threading.Tasks;
using GoLocal.Identity.Application.Commands.Users.ResetPasswordConfirmation;
using GoLocal.Shared.Bus.Results.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoLocal.Identity.Api.Pages.Account
{
    public class ResetPasswordConfirmation : PageModel
    {
        private readonly IMediator _mediator;

        public ResetPasswordConfirmation(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty(SupportsGet = true)]
        public string Token { get; set; }
        
        public void OnGet()
        {
            Command = new ResetPasswordConfirmationCommand
            {
                Token = Token
            };
        }
        
        [BindProperty]
        public ResetPasswordConfirmationCommand Command { get; set; }

        public async Task<IActionResult> OnPost()
        {
            var result = await _mediator.Send(Command);
            if (result.Status != ResultStatus.Ok)
                return BadRequest(result.Message);

            return RedirectToPage(nameof(Login));
        }
    }
}