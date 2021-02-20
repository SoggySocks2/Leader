using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Smeat.Leader.Core.Entities.CustomerAggregate;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Smeat.Leader.Web.Areas.Customer.Pages
{
    public class DetailsModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Id")]
            public long Id { get; set; }

            [Required(ErrorMessage = "{0} is required")]
            [StringLength(100)]
            [EmailAddress]
            [Display(Name = "First name")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "{0} is required")]
            [StringLength(100)]
            [DataType(DataType.Text)]
            [Display(Name = "Last name")]
            public string LastName { get; set; }

            public Address Address { get; internal set; }

            public IReadOnlyCollection<Vehicle> Vehicles { get; internal set; }
        }


        public void OnGet(long? customerId = null)
        {
            var customer = new Core.Entities.CustomerAggregate.Customer(1);
            customer.Id = 1;
            customer.FirstName = "Peter";
            customer.LastName = "Jones";
            customer.SetAddress(new Address() { Line1 = "16 Pentywyn Heights", Line2 = "Deganwy" });
            customer.AddVehicle(new Vehicle() { Id = 1, Make = "BMW", Model = "320d", Year = 2016, VRM = "SM03 EAT" });
            customer.AddVehicle(new Vehicle() { Id = 2, Make = "Mazda", Model = "3", Year = 2015, VRM = "LC64 HWV" });

            Input = new InputModel()
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                Vehicles = customer.Vehicles
            };
        }
    }
}
