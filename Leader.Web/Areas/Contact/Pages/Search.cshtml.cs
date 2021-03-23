using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Smeat.Leader.Infrastructure.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Smeat.Leader.Web.Areas.Contact.Pages
{
    public class SearchModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;
        public SearchModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "{0} is required")]
            [MinLength(1, ErrorMessage = "Min 1 characters")]
            [StringLength(100)]
            [Display(Name = "Criteria")]
            public string Criteria { get; set; }

            public List<CustomerSearchResultDTO> SearchResults = new List<CustomerSearchResultDTO>();


            public Web.Pages.Shared._PaginationModel PaginationInfo { get; set; } = new Web.Pages.Shared._PaginationModel(0, 0, 1);
        }

        public class CustomerSearchResultDTO
        {
            public long Id { get; internal set; }
            public string FirstName { get; internal set; }
            public string LastName { get; internal set; }
        }

        public void OnGet()
        {
            Input = new InputModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            int skip = 0;
            int take = 10;

            if (skip < 0) skip = 0;
            if (take < 1) take = 1;

            var customers = await _customerRepository.GetByName(Input.Criteria, skip, take);
            var customersTotal = await _customerRepository.GetByName_CountAsync(Input.Criteria);

            Input.PaginationInfo = new Web.Pages.Shared._PaginationModel(customersTotal, skip, take);

            foreach (var customer in customers)
            {
                Input.SearchResults.Add(new CustomerSearchResultDTO { Id = customer.Id, FirstName = customer.FirstName, LastName = customer.LastName });
            }

            if (customers.Count == 0)
            {
                @ViewData["No Results"] = "No Results";
            }

            return Page();
        }
    }
}
