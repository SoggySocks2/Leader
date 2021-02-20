using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

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

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "{0} is required")]
            [StringLength(100)]
            [EmailAddress]
            [Display(Name = "Search")]
            public string Search { get; set; }
        }

        public void OnGet()
        {
            _logger.LogDebug("Entering Index OnGet() method");

            ViewData["Welcome"] = _localizer["Welcome"];
        }
    }
}
