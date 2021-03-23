using System.Collections.Generic;

namespace Smeat.Leader.Core.Entities
{
    public class Addresses : List<Address>
    {
        #region Validation

        /// <summary>
        /// An indicator of the validity of this object. A positive indicates all editable business rules have been satisfied
        /// </summary>
        public bool IsValid()
        {
            foreach (var item in this)
            {
                if (!item.IsValid()) return false;
            }
            return true;
        }

        #endregion
    }
}
