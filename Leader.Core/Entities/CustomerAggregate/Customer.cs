namespace Smeat.Leader.Core.Entities.CustomerAggregate
{
    public class Customer : EntityBase<Customer>
    {
        #region Properties

        public string FirstName { get; set; }

        #endregion

        #region Constructors

        public Customer(): base()
        {
            FirstName = string.Empty;
        }

        #endregion

        #region Validation

        /// <summary>
        /// An indicator of the validity of this object. A positive indicates all editable business rules have been satisfied
        /// </summary>
        public bool IsValid()
        {
            Validate();
            //return BrokenRules.Count == 0 && Address.IsValid();
            return BrokenRules.Count == 0;
        }

        /// <summary>
        /// Determine any broken business rules
        /// </summary>
        public void Validate()
        {
            BrokenRules.AddRule("reqFirstName", "FirstName is required", string.IsNullOrWhiteSpace(FirstName));
        }

        #endregion
    }
}
