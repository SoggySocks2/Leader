using System.Collections.Generic;

namespace Smeat.Leader.Core.Entities.CustomerAggregate
{
    public class Vehicles : List<Vehicle>
    {
        #region Validation

        /// <summary>
        /// An indicator of the validity of this object. A positive indicates all editable business rules have been satisfied
        /// </summary>
        public bool IsValid()
        {
            foreach(var vehicle in this)
            {
                if (!vehicle.IsValid()) return false;
            }
            return true;
        }

        #endregion
    }
}
