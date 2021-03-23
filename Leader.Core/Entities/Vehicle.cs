using Smeat.Leader.SharedKernel.Base;
using System;

namespace Smeat.Leader.Core.Entities
{
    public class Vehicle : BaseEntity<Vehicle>
    {
        #region Variables

        private string _make;
        private string _model;
        private int? _year;
        private string _vrm;

        #endregion

        #region Properties

        public long CustomerId { get; private set; }
        public Customer Customer { get; set; }

        public string Make
        {
            get { return _make; }
            set
            {
                if (_make != value)
                {
                    _make = value;
                    MarkDirty();
                }
            }
        }

        public string Model
        {
            get { return _model; }
            set
            {
                if (_model != value)
                {
                    _model = value;
                    MarkDirty();
                }
            }
        }

        public int? Year
        {
            get { return _year; }
            set
            {
                if (_year != value)
                {
                    _year = value;
                    MarkDirty();
                }
            }
        }

        public string VRM
        {
            get { return _vrm; }
            set
            {
                if (_vrm != value)
                {
                    _vrm = value;
                    MarkDirty();
                }
            }
        }

        #endregion

        #region Constructors

        public Vehicle(long customerId) : base()
        {
            CustomerId = customerId;
            _make = string.Empty;
            _model = string.Empty;
            _year = null;
            _vrm = string.Empty;
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
            BrokenRules.AddRule("reqMake", "Make is required", string.IsNullOrWhiteSpace(Make));
            BrokenRules.AddRule("invYear", "Year can not be in the future", Year != null && Year > DateTime.Now.Year);
        }

        #endregion
    }
}
