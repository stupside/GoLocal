using System.Threading.Tasks;
using GoLocal.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoLocal.Identity.Api.Pages.Account
{
    public class Register : PageModel
    {
        private readonly SignInManager<User> _sign;
        private readonly UserManager<User> _user;

        public Register(UserManager<User> user, SignInManager<User> sign)
        {
            _user = user;
            _sign = sign;
        }
        
        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; init; }

        public IActionResult OnGet()
            => Page();
        
        [BindProperty]
        public string Email { get; init; }
        [BindProperty]
        public string Username { get; init; }
        [BindProperty]
        public string Password { get; init; }
        [BindProperty]
        public string PasswordConfirmation { get; init; }
        
        public async Task<IActionResult> OnPost()
        {
            if (Password != PasswordConfirmation)
            {
                ModelState.AddModelError(string.Empty, "Passwords doesn't match");
                return RedirectToPage();
            }

            User user = new User(Email, Username);
            
            var result = await _user.CreateAsync(user, Password);
            if (result.Succeeded)
            {
                RedirectToPage(nameof(Login), new { ReturnUrl });
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            
            return RedirectToPage();
        }
    }
}