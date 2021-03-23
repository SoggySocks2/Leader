using Smeat.Leader.SharedKernel.Base;

namespace Smeat.Leader.Core.Entities
{
    public class Group : BaseEntity<Customer>
    {
        #region Variables

        private string _groupName;

        #endregion

        #region Properties

        public string GroupName
        {
            get { return _groupName; }
            set
            {
                if (_groupName != value)
                {
                    _groupName = value;
                    MarkDirty();
                }
            }
        }
        public Customers Customers { get; private set; }

        #endregion

        #region Constructors

        public Group() : base()
        {
            _groupName = string.Empty;
            Customers = new Customers();
        }

        #endregion

        #region Validation

        /// <summary>
        /// An indicator of the validity of this object. A positive indicates all editable business rules have been satisfied
        /// </summary>
        public override bool IsValid()
        {
            return base.IsValid() && Customers.IsValid();
        }

        /// <summary>
        /// Determine any broken business rules
        /// </summary>
        public override void Validate()
        {
            BrokenRules.AddRule("reqGroupName", "GroupName is required", string.IsNullOrWhiteSpace(GroupName));
        }

        #endregion
    }
}
