using System;
using GoLocal.Shared.Bus.Commons.Filtering;
using GoLocal.Shared.Bus.Results.Pages;

namespace GoLocal.Shared.Bus.Commons.Mediator
{
    public abstract class AbstractPagedRequest<TEntity, TResponse> : AbstractRequest<Page<TResponse>>, IFilter<TEntity>
    {
        public int Take { get; init; }
        public int Skip { get; init; }
        public Search<TEntity> Search { get; init; }
        public Order<TEntity> Order { get; init; }

        protected AbstractPagedRequest()
        {
            Order = new Order<TEntity>();
            Search = new Search<TEntity>();
        }

        protected void ConfigureOrder(Action<Order<TEntity>> configuration)
            => configuration.Invoke(Order);
        
        protected void ConfigureSearch(Action<Search<TEntity>> configuration)
            => configuration.Invoke(Search);
        
    }
}