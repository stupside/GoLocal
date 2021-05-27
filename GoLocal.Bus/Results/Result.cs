using System;
using GoLocal.Bus.Results.Enums;

namespace GoLocal.Bus.Results
{
    public class Result : AbstractResult<Result>
    {
        public Result<TResult> ToResult<TResult>()
        {
            return new Result<TResult>()
                .WithMessage(Message);
        }
    }

    public class Result<TEntity> : AbstractResult<Result<TEntity>>
    {
        private object _entity;
        public override object Entity {
            get
            {
                if (Status != ResultStatus.Ok)
                    throw new InvalidOperationException(
                        $"The entity is not accessible. Status of the result was set to {this.Status}");

                return _entity;
            }
        }
        public TEntity TypedEntity => (TEntity)_entity;

        public Result<TEntity> WithEntity(object entity)
        {
            _entity = entity;
            return this;
        }
    }
}