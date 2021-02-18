using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Smeat.Leader.Web.Areas.Culture.Pages
{
    public class SetCultureModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost(string culture, string returnUrl = null)
        {

            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });

            return LocalRedirect(returnUrl);
        }
    }
}
