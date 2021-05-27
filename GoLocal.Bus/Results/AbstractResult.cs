using System;
using System.Collections.Generic;
using GoLocal.Bus.Results.Enums;

namespace GoLocal.Bus.Results
{
    public abstract class AbstractResult
    {
        public virtual object Entity {
            get
            {
                if (Status != ResultStatus.Ok)
                    throw new InvalidOperationException(
                        $"The entity is not accessible. Status of the result was set to {this.Status}");

                return null;
            }
        }
        public ResultStatus Status { get; set; }
        public string Message { get; set; }
        public List<object> Errors { get; set; }

        protected AbstractResult()
        {
            Errors = new List<object>();
        }
    }

    public abstract class AbstractResult<TEntity> : AbstractResult
        where TEntity : AbstractResult<TEntity>
    {
        public TEntity WithErrors(IEnumerable<object> errors)
        {
            if(errors != null)
                Errors.AddRange(errors);
            
            return (TEntity)this;
        }

        public TEntity WithMessage(string message)
        {
            Message = message;
            return (TEntity)this;
        }
        
        public TEntity WithStatus(ResultStatus status)
        {
            Status = status;
            return (TEntity)this;
        }
    }
}