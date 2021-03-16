using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Smeat.Leader.Infrastructure.Identity;
using Smeat.Leader.SharedKernel.Identity;

namespace Smeat.Leader.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<LeaderUser> _userManager;
        private readonly IStringLocalizer<ConfirmEmailModel> _stringLocalizer;

        public string CurrentUICulture { get; set; }

        public ConfirmEmailModel(UserManager<LeaderUser> userManager, IStringLocalizer<ConfirmEmailModel> stringLocalizer)
        {
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                var msg = _stringLocalizer["Unable to load user with ID"];
                return NotFound(string.Format("{0} {1}.", msg, userId));
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
            {
                var msg = _stringLocalizer["Error confirming email for user with ID"];
                throw new InvalidOperationException(string.Format("{0} {1}.", msg, userId));
            }

            CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            return Page();
        }
    }
}
