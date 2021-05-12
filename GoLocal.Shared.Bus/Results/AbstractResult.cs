using GoLocal.Shared.Bus.Results.Enums;
using GoLocal.Shared.Bus.Results.Interfaces;

namespace GoLocal.Shared.Bus.Results
{
    public abstract class AbstractResult<TEntity> : IResult<TEntity>
    {
        public ResultType Type { get; private set; }
        public string Message { get; private set; }
        public TEntity Entity { get; private set; }
        
        protected AbstractResult(ResultType type, string message)
        {
            Type = type;
            Message = message;
        }
        
        protected AbstractResult(ResultType type, TEntity entity = default)
        {
            Type = type;
            Entity = entity;
        }

        protected AbstractResult(TEntity entity = default)
            : this(ResultType.Ok, entity)
        {
        }
    }
}