using GoLocal.Shared.Bus.Results.Enums;
using MediatR;

namespace GoLocal.Shared.Bus.Results
{
    public class Result<TEntity> : AbstractResult<TEntity>
    {
        public Result(ResultType type, string message) : base(type, message)
        {
        }

        public Result(ResultType type, TEntity entity = default) : base(type, entity)
        {
        }

        public Result(TEntity entity = default) : base(entity)
        {
        }
    }

    public class Result : Result<Unit>
    {
        public Result(ResultType type, string message) : base(type, message)
        {
        }

        public Result(ResultType type, Unit entity = default) : base(type, entity)
        {
        }

        public Result(Unit entity = default) : base(entity)
        {
        }
    }
}