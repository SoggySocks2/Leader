using Smeat.Leader.SharedKernel.Base;
using System;

namespace Smeat.Leader.Core.Entities
{
    public class Vehicle : BaseEntity<Vehicle>
    {
        #region Properties

        public string Make { get; set; }
        public string Model { get; set; }
        public int? Year { get; set; }
        public string VRM { get; set; }

        #endregion

        #region Constructors

        public Vehicle()
        {
            Make = string.Empty;
            Model = string.Empty;
            Year = null;
            VRM = string.Empty;
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
            BrokenRules.AddRule("reqMake", "Make is required", string.IsNullOrWhiteSpace(Make));
            BrokenRules.AddRule("invYear", "Year can not be in the future", Year != null && Year > DateTime.Now.Year);
        }

        #endregion
    }
}
