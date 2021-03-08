using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Smeat.Leader.Web.Areas.Contact.Pages
{
    public class SearchModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "{0} is required")]
            [MinLength(2, ErrorMessage = "Min 2 characters")]
            [StringLength(100)]
            [Display(Name = "Criteria")]
            public string Criteria { get; set; }
        }

        public void OnGet()
        {
            Input = new InputModel();
        }
    }
}
