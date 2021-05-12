using GoLocal.Shared.Bus.Results.Pages;

namespace GoLocal.Shared.Bus.Commons.Filtering
{
    public interface IFilter
    {
        int Take { get; }
        int Skip { get; }
    }
    
    public interface IFilter<TEntity> : IFilter
    {
        Search<TEntity> Search { get; }
        Order<TEntity> Order { get; }
    }
}