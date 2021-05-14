using System.Threading.Tasks;
using GoLocal.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoLocal.Identity.Api.Pages.Account
{
    
    public class Login : PageModel
    {
        private readonly SignInManager<User> _sign;

        public Login(SignInManager<User> sign)
        {
            _sign = sign;
        }
        
        
        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; init; }
        

        [BindProperty]
        public string Username { get; init; }
        [BindProperty]
        public string Password { get; init; }

        public IActionResult OnGet()
        {
            return Page();
        }
        
        public async Task<IActionResult> OnPost()
        {
            User user = await _sign.UserManager.FindByNameAsync(Username);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return RedirectToPage();
            }

            if (!await _sign.UserManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError(string.Empty, "Email not confirmed");
                return RedirectToPage();
            }
            
            var result = await _sign.PasswordSignInAsync(Username, Password, false, true);
            
            if (result.Succeeded)
            {
                return Redirect(ReturnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage(nameof(LoginTfa), new {ReturnUrl});
            }
            if (result.IsNotAllowed)
            {
                return Unauthorized();
            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToPage(nameof(LoginTfa), new {ReturnUrl});
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return RedirectToPage();
        }
    }
}