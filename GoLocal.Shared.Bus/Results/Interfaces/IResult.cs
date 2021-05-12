using GoLocal.Shared.Bus.Results.Enums;

namespace GoLocal.Shared.Bus.Results.Interfaces
{
    public interface IResult
    {
        ResultType Type { get; }
        string Message { get; }
    }

    public interface IResult<out TEntity> : IResult
    {
        TEntity Entity { get; }
    }
}