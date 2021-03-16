using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Smeat.Leader.SharedKernel.Identity;

namespace Smeat.Leader.Web.Areas.Identity.Pages.Secured
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager<LeaderUser> _userManager;
        private readonly ILogger<ProfileModel> _logger;
        private readonly IStringLocalizer<ProfileModel> _stringLocalizer;

        public ProfileModel(UserManager<LeaderUser> userManager, ILogger<ProfileModel> logger, IStringLocalizer<ProfileModel> localizer)
        {
            _userManager = userManager;
            _logger = logger;
            _stringLocalizer = localizer;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "{0} is required")]
            [StringLength(30)]
            [DataType(DataType.Text)]
            [Display(Name = "First name")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "{0} is required")]
            [StringLength(30)]
            [DataType(DataType.Text)]
            [Display(Name = "Last name")]
            public string LastName { get; set; }
        }

        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null) user = new LeaderUser();
            Input = new InputModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    ViewData["Message"] = _stringLocalizer["Update succeeded"];
                }
                else
                {
                    ModelState.AddModelError(string.Empty, _stringLocalizer["Failed to update user."]);
                    ViewData["Message"] = _stringLocalizer["Failed to update user."];
                }
            }

            return Page();
        }
    }
}
