using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Smeat.Leader.Infrastructure.Identity;

namespace Smeat.Leader.Web.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<LeaderUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IStringLocalizer<LoginModel> _stringLocalizer;

        public LoginModel(SignInManager<LeaderUser> signInManager, ILogger<LoginModel> logger, IStringLocalizer<LoginModel> stringLocalizer)
        {
            _signInManager = signInManager;
            _logger = logger;
            _stringLocalizer = stringLocalizer;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "{0} is required")]
            [EmailAddress]
            [Display(Name = "Email")]
            [StringLength(100)]
            public string Email { get; set; }

            [Required(ErrorMessage = "{0} is required")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            [StringLength(100)]
            public string Password { get; set; }

            [Display(Name = "Remember me")]
            public bool RememberMe { get; set; }
        }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                //var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, true);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    //await TransferAnonymousBasketToUserAsync(Input.Email);
                    return LocalRedirect(returnUrl);
                }
                //if (result.RequiresTwoFactor)
                //{
                //    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                //}
                //if (result.IsLockedOut)
                //{
                //    _logger.LogWarning("User account locked out.");
                //    return RedirectToPage("./Lockout");
                //}
                else if (result.IsNotAllowed)
                {
                    ModelState.AddModelError(string.Empty, _stringLocalizer["Invalid login attempt. Please check your email and confirm your email address then try again."]);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, _stringLocalizer["Invalid login attempt."]);
                }
                return Page();
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
