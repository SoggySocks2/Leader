using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Smeat.Leader.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHtmlLocalizer _localizer;

        public IndexModel(ILogger<IndexModel> logger, IHtmlLocalizer<IndexModel> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        public void OnGet()
        {
            _logger.LogDebug("Entering Index OnGet() method");

            ViewData["Welcome"] = _localizer["Welcome"];
        }
    }
}
