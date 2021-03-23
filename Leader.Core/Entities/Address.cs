using Smeat.Leader.SharedKernel.Base;

namespace Smeat.Leader.Core.Entities
{
    public class Address : BaseEntity<Address>
    {
        #region Variables

        private string _line1;
        private string _line2;
        private string _town;
        private string _county;
        private string _region;
        private string _postCode;

        #endregion

        #region Properties

        public long CustomerId { get; private set; }
        public Customer Customer { get; set; }

        public string Line1
        {
            get { return _line1; }
            set
            {
                if (_line1 != value)
                {
                    _line1 = value;
                    MarkDirty();
                }
            }
        }

        public string Line2
        {
            get { return _line2; }
            set
            {
                if (_line2 != value)
                {
                    _line2 = value;
                    MarkDirty();
                }
            }
        }
        
        public string Town
        {
            get { return _town; }
            set
            {
                if (_town != value)
                {
                    _town = value;
                    MarkDirty();
                }
            }
        }

        public string County
        {
            get { return _county; }
            set
            {
                if (_county != value)
                {
                    _county = value;
                    MarkDirty();
                }
            }
        }

        public string Region
        {
            get { return _region; }
            set
            {
                if (_region != value)
                {
                    _region = value;
                    MarkDirty();
                }
            }
        }

        public string PostCode
        {
            get { return _postCode; }
            set
            {
                if (_postCode != value)
                {
                    _postCode = value;
                    MarkDirty();
                }
            }
        }

        #endregion

        #region Constructors

        public Address(long customerId) : base()
        {
            CustomerId = customerId;
            Line1 = string.Empty;
            Line2 = string.Empty;
            Town = string.Empty;
            County = string.Empty;
            Region = string.Empty;
            PostCode = string.Empty;
        }

        #endregion

        #region Validation

        /// <summary>
        /// An indicator of the validity of this object. A positive indicates all editable business rules have been satisfied
        /// </summary>
        public override bool IsValid()
        {
            return base.IsValid(); // && Address.IsValid() && _vehicles.IsValid();
        }

        /// <summary>
        /// Determine any broken business rules
        /// </summary>
        public override void Validate()
        {
            BrokenRules.AddRule("reqCustomerId", "Customer Id is required", CustomerId < 1);
            BrokenRules.AddRule("reqLine1", "Dealer Id is required", string.IsNullOrWhiteSpace(Line1));
        }

        #endregion
    }
}
