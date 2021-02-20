using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Smeat.Leader.Core.Entities.CustomerAggregate
{
    public class Customer : BaseEntity<Customer>
    {
        #region Properties

        public long DealerId { get; private set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100)]
        [DataType(DataType.Text)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100)]
        [DataType(DataType.Text)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public Address Address { get; private set; }
        
        private readonly Vehicles _vehicles;
        public IReadOnlyCollection<Vehicle> Vehicles => _vehicles.AsReadOnly();

        #endregion

        #region Constructors

        public Customer(long dealerId): base()
        {
            DealerId = dealerId;
            FirstName = string.Empty;
            LastName = string.Empty;
            Address = new Address();
            _vehicles = new Vehicles();
        }

        #endregion

        #region Add Vehicle

        public void AddVehicle(Vehicle vehicle)
        {
            if (!Vehicles.Any(v => v.Id == vehicle.Id))
            {
                _vehicles.Add(vehicle);
                return;
            }
        }

        #endregion

        public void SetAddress(Address address)
        {
            Address = address;
        }

        #region Validation

        /// <summary>
        /// An indicator of the validity of this object. A positive indicates all editable business rules have been satisfied
        /// </summary>
        public bool IsValid()
        {
            Validate();
            return BrokenRules.Count == 0 && Address.IsValid() && _vehicles.IsValid();
        }

        /// <summary>
        /// Determine any broken business rules
        /// </summary>
        public void Validate()
        {
            BrokenRules.AddRule("reqDealerId", "Dealer Id is required", DealerId < 1);
            BrokenRules.AddRule("reqFirstName", "FirstName is required", string.IsNullOrWhiteSpace(FirstName));
            BrokenRules.AddRule("reqLastName", "LastName is required", string.IsNullOrWhiteSpace(LastName));
        }

        #endregion
    }
}
