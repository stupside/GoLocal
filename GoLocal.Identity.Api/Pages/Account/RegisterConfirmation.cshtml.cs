using System.Threading.Tasks;
using GoLocal.Identity.Application.Commands.Users.CreateUserConfirmation;
using GoLocal.Shared.Bus.Results.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoLocal.Identity.Api.Pages.Account
{
    public class RegisterConfirmation : PageModel
    {
        private readonly IMediator _mediator;

        public RegisterConfirmation(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [BindProperty(SupportsGet = true)]
        public string Uid { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string Token { get; set; }
        
        public async Task<IActionResult> OnGet()
        {
            var result = await _mediator.Send(new CreateUserConfirmationCommand(Token, Uid));
            if (result.Status != ResultStatus.Ok)
                return BadRequest(result.Message);

            return RedirectToPage();
        }
    }
}