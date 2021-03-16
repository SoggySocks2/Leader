using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Smeat.Leader.Infrastructure.Services;
using Smeat.Leader.SharedKernel.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Smeat.Leader.Web.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<LeaderUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<RegisterModel> _stringLocalizer;

        public RegisterModel(
            UserManager<LeaderUser> userManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IStringLocalizer<RegisterModel> stringLocalizer
            )
        {
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
            _stringLocalizer = stringLocalizer;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "{0} is required")]
            [StringLength(100)]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "{0} is required")]
            [StringLength(100)]
            [DataType(DataType.Text)]
            [Display(Name = "First name")]
            public string FirstName { get; set; }

            [Required (ErrorMessage = "{0} is required")]
            [StringLength(100)]
            [DataType(DataType.Text)]
            [Display(Name = "Last name")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "{0} is required")]
            [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required(ErrorMessage = "{0} is required")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            Input = new InputModel();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new LeaderUser { UserName = Input.Email, Email = Input.Email, FirstName = Input.FirstName, LastName = Input.LastName };
                //var result = await _userManager.CreateAsync(user, Input.Password);

                /* Temp for debugging */
                IdentityResult result;
                try
                {
                    result = await _userManager.CreateAsync(user, Input.Password);
                }
                catch (System.Exception ex)
                {
                    @ViewData["Message"] = "Error: " + ex.Message;
                    return Page();
                }

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    /* Ensure the user sees the correct culture when clicking conform email */
                    var currentCulture = System.Threading.Thread.CurrentThread.CurrentUICulture;

                    await _emailSender.SendEmailAsync(new string[] { Input.Email }, _stringLocalizer["Confirm your email"],
                        string.Format("{0} {1}<p/> {2} <a href='{3}&culture={4}'>{5}</a>", _stringLocalizer["Hello"], Input.FirstName + " " + Input.LastName, _stringLocalizer["Please confirm your account by"], HtmlEncoder.Default.Encode(callbackUrl), currentCulture, _stringLocalizer["clicking here"])
                        );

                    @ViewData["Message"] = _stringLocalizer["Thank you for registering. You have been sent an email to confirm your account. Please click the link in the email to confirm your account and log in"];
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
