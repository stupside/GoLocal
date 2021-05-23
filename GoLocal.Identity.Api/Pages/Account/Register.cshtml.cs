using System.Threading.Tasks;
using GoLocal.Identity.Application.Commands.Users.CreateUser;
using GoLocal.Shared.Bus.Results.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoLocal.Identity.Api.Pages.Account
{
    public class Register : PageModel
    {
        private readonly IMediator _mediator;

        public Register(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult OnGet()
            => Page();

        [BindProperty(SupportsGet = true)] public string ReturnUrl { get; set; }
        [BindProperty] public CreateUserCommand Command { get; set; }

        public async Task<IActionResult> OnPost()
        {
            var result = await _mediator.Send(Command.SetCallback(Url.PageLink(nameof(RegisterConfirmation))));
            
            if (result.Status != ResultStatus.Ok)
                return RedirectToPage();

            return RedirectToPage(nameof(Login), new { ReturnUrl });
        }
    }
}
