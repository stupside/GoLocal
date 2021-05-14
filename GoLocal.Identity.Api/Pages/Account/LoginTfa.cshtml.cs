using System.Threading.Tasks;
using GoLocal.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoLocal.Identity.Api.Pages.Account
{
    public class LoginTfa : PageModel
    {
        private readonly SignInManager<User> _sign;

        public LoginTfa(SignInManager<User> sign)
        {
            _sign = sign;
        }
        
        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }
        
        public void OnGet()
        {
        }

        [BindProperty]
        public string Code { get; set; }
        
        public async Task<IActionResult> OnPost()
        {
            User user = await _sign.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
                return NotFound();

            var result = await _sign.TwoFactorAuthenticatorSignInAsync(Code, false, false);

            if (result.Succeeded)
                return Redirect(ReturnUrl);
            
            if (result.IsLockedOut)
                return RedirectToPage(nameof(Login), new {ReturnUrl});

            if (result.RequiresTwoFactor)
                return RedirectToPage();
                
            if(result.IsNotAllowed)
                return Unauthorized();

            return NotFound();
        }
    }
}