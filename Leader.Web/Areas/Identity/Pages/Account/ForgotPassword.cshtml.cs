using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Localization;
using Smeat.Leader.SharedKernel.Identity;

namespace Smeat.Leader.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<LeaderUser> _userManager;
        private readonly Infrastructure.Services.IEmailSender _emailSender;
        private readonly IStringLocalizer<ForgotPasswordModel> _stringLocalizer;

        public ForgotPasswordModel(UserManager<LeaderUser> userManager, Infrastructure.Services.IEmailSender emailSender, IStringLocalizer<ForgotPasswordModel> stringLocalizer)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _stringLocalizer = stringLocalizer;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "{0} is required")]
            [StringLength(100)]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                /* Ensure the user sees the correct culture when clicking conform email */
                var currentCulture = System.Threading.Thread.CurrentThread.CurrentUICulture;

                await _emailSender.SendEmailAsync(new string[] { Input.Email }, _stringLocalizer["Reset Password"],
                    string.Format("{0} {1}<p/> {2} <a href='{3}&culture={4}'>{5}</a>", _stringLocalizer["Hello"], Input.Email, _stringLocalizer["Please reset your password by"], HtmlEncoder.Default.Encode(callbackUrl), currentCulture, _stringLocalizer["clicking here"])
                    );

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
