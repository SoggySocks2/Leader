namespace Smeat.Leader.Core.Entities
{
    public abstract class EntityBase<T>
    {
        #region Properties

        public virtual long Id { get; protected set; }

        #endregion

        #region Constructors

        public EntityBase()
        {
            Id = 0;
        }

        #endregion

        #region Validation

        /// <summary>
        /// A collection of business rules that have not been satisfied
        /// </summary>
        public Validation.BrokenRules.IBrokenRulesCollection BrokenRules { get; protected set; }

        #endregion
    }
}
