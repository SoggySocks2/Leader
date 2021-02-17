using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Smeat.Leader.Infrastructure.Identity;

namespace Smeat.Leader.Web.Areas.Identity.Pages.Account
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager<LeaderUser> _userManager;
        private readonly ILogger<LoginModel> _logger;

        public ProfileModel(UserManager<LeaderUser> userManager, ILogger<LoginModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [Display(Name = "First name")]
            public string Firstname { get; set; }

            [Required]
            [Display(Name = "Last name")]
            public string Lastname { get; set; }
        }

        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            Input = new InputModel();
            Input.Email = user.Email;
            Input.Firstname = user.FirstName;
            Input.Lastname = user.LastName;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                user.FirstName = Input.Firstname;
                user.LastName = Input.Lastname;
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    ViewData["Message"] = "Update succeeded";
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to update user.");
                    ViewData["Message"] = "Failed to update user.";
                }
            }

            return Page();
        }
    }
}
