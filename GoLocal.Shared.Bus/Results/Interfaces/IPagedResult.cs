using GoLocal.Shared.Bus.Results.Pages;

namespace GoLocal.Shared.Bus.Results.Interfaces
{
    public interface IPagedResult<TEntity> : IResult<Page<TEntity>>
    {
        
    }
}