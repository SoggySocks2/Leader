using System.ComponentModel.DataAnnotations.Schema;

namespace Smeat.Leader.Core.Entities
{
    public abstract class BaseEntity<T>
    {
        #region Properties

        //public virtual long Id { get; protected set; }
        public virtual long Id { get; set; }

        #endregion

        #region Constructors

        public BaseEntity()
        {
            Id = 0;
        }

        #endregion

        #region Validation

        /// <summary>
        /// A collection of business rules that have not been satisfied
        /// </summary>
        [NotMapped]
        public Validation.BrokenRules.IBrokenRulesCollection BrokenRules { get; protected set; }

        #endregion
    }
}
