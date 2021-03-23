using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smeat.Leader.SharedKernel.Base
{
    public abstract class BaseEntity<T>
    {
        #region Variable Declarations

        private bool _isDirty = false;

        #endregion

        #region Properties

        [Key]
        public virtual long Id { get; protected set; }
        //public virtual long Id { get; set; }

        #endregion

        #region Constructors

        public BaseEntity()
        {
            Id = 0;
            _isDirty = false;
            BrokenRules = new Validation.BrokenRules.BrokenRulesCollection();
        }

        #endregion

        #region Business Properties and Methods

        /// <summary>
        /// Object has changed.
        /// </summary>
        protected void MarkDirty()
        {
            _isDirty = true;
        }
        /// <summary>
        /// Object has just be created or loaded.
        /// </summary>
        internal void MarkClean()
        {
            _isDirty = false;
        }
        /// <summary>
        /// Property of field has changed.
        /// </summary>
        internal virtual bool IsDirty
        {
            get { return _isDirty; }
        }

        #endregion

        #region Validation Rules

        /// <summary>
        /// A collection of business rules that have not been satisfied
        /// </summary>
        [NotMapped]
        public Validation.BrokenRules.IBrokenRulesCollection BrokenRules { get; private set; }

        /// <summary>
        /// is the object in a valid state.
        /// </summary>
        public virtual bool IsValid()
        {
            //Remove all existing broken rules.
            BrokenRules.Clear();

            //Check for current broken rules.
            Validate();

            //If we have no broken rules then we have a valid object.
            return BrokenRules.Count == 0;
        }

        /// <summary>
        /// checks the objects validation rules.
        /// </summary>
        public abstract void Validate();
        //{
        //    throw new NotImplementedException("Validate() method not implemented.");
        //}

        #endregion
    }
}
