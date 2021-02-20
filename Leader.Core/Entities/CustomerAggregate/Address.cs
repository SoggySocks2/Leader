namespace Smeat.Leader.Core.Entities.CustomerAggregate
{
    public class Address : BaseEntity<Address>
    {
        #region Properties

        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string Region { get; set; }
        public string PostCode { get; set; }

        #endregion

        #region Constructors

        public Address()
        {
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
        public bool IsValid()
        {
            Validate();
            return BrokenRules.Count == 0;
        }

        /// <summary>
        /// Determine any broken business rules
        /// </summary>
        public void Validate()
        {
            BrokenRules.AddRule("reqLine1", "Dealer Id is required", string.IsNullOrWhiteSpace(Line1));
        }

        #endregion
    }
}
