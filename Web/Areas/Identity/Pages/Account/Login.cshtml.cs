using Application.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAuthentificationService _authentificationService;

        public LoginModel(IAuthentificationService authentificationService)
        {
            _authentificationService = authentificationService;
        }
        [Required]
        [EmailAddress]
        [BindProperty]
        public string? Email { get; set; }

        [BindProperty]
        [Required]
        public string? Password { get; set; }

        public string? Error { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var principal = _authentificationService.TryGetClaimPrincipal(Email, Password);
                if (principal != null)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return LocalRedirect(returnUrl);
                }
                else Error = "Invalid login or password";
            }
            
            return Page();
        }
    }
}
