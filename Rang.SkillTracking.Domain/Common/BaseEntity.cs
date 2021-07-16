using System;
using System.Collections.Generic;

namespace Rang.SkillTracking.Domain.Common
{
    public abstract class BaseEntity<Tmodel>
         where Tmodel : BaseModel
    {
        //fields
        protected readonly Tmodel _model;

    
        //properties
        public bool IsValid { get => ValidateMe(); }
        
        public IDictionary<ModelValidationStatusCode, List<string>> ModelValidationErrors { get; protected set; }


        //constructors
        protected BaseEntity(Tmodel model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            
            ModelValidationErrors = new Dictionary<ModelValidationStatusCode, List<string>>();
            InitializeMe();
        }

        public BaseEntity()
        {
           
        }


        //methods
        public abstract Tmodel GetModel();
        
        protected abstract bool ValidateMe();
        
        protected abstract void InitializeMe();
        
        protected void AddModelValidationError(ModelValidationStatusCode statusCode, string validationMessage)
        {
            if (ModelValidationErrors.ContainsKey(statusCode))
            {
                ModelValidationErrors[statusCode].Add(validationMessage);
            }
            else
            {
                var messageList = new List<string> { validationMessage };
                ModelValidationErrors.Add(statusCode, messageList);
            }
        }
    }
}
