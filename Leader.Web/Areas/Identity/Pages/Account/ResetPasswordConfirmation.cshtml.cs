using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Smeat.Leader.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordConfirmationModel : PageModel
    {
        public string CurrentUICulture { get; set; }

        public void OnGet()
        {
            CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
        }
    }
}
