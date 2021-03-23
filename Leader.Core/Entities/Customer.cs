using Smeat.Leader.SharedKernel.Base;

namespace Smeat.Leader.Core.Entities
{
    public class Customer : BaseEntity<Customer>
    {
        #region Variables

        private string _firstName;
        private string _lastName;

        #endregion

        #region Properties

        public long GroupId { get; private set; }
        public Group Group { get; set; }

        public string FirstName 
        { 
            get { return _firstName; }
            set
            {
                if(_firstName != value)
                {
                    _firstName = value;
                    MarkDirty();
                }
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    MarkDirty();
                }
            }
        }

        public Addresses Addresses { get; private set; }
        
        //private readonly Vehicles _vehicles;
        //public IReadOnlyCollection<Vehicle> Vehicles => _vehicles.AsReadOnly();
        public Vehicles Vehicles { get; private set; }

        #endregion

        #region Constructors

        public Customer(long groupId): base()
        {
            GroupId = groupId;
            _firstName = string.Empty;
            _lastName = string.Empty;
            Addresses = new Addresses();
            Vehicles = new Vehicles();            
        }

        #endregion

        #region Validation

        /// <summary>
        /// An indicator of the validity of this object. A positive indicates all editable business rules have been satisfied
        /// </summary>
        public override bool IsValid()
        {
            return base.IsValid() && Addresses.IsValid() && Vehicles.IsValid();
        }

        /// <summary>
        /// Determine any broken business rules
        /// </summary>
        public override void Validate()
        {
            BrokenRules.AddRule("reqGroupId", "Group Id is required", GroupId < 1);
            BrokenRules.AddRule("reqFirstName", "FirstName is required", string.IsNullOrWhiteSpace(FirstName));
            BrokenRules.AddRule("reqLastName", "LastName is required", string.IsNullOrWhiteSpace(LastName));
        }

        #endregion
    }
}
