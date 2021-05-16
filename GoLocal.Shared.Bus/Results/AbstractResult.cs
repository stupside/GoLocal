using System;
using System.Collections.Generic;
using GoLocal.Shared.Bus.Results.Enums;

namespace GoLocal.Shared.Bus.Results
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
        public ResultStatus Status { get; internal set; }
        public string Message { get; internal set; }
        public List<object> Errors { get; internal set; }

        protected AbstractResult()
        {
            Errors = new List<object>();
        }
    }

    public abstract class AbstractResult<TEntity> : AbstractResult
        where TEntity : AbstractResult<TEntity>
    {
        public TEntity WithError(object error)
        {
            Errors.Add(error);
            return (TEntity)this;
        }
        
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