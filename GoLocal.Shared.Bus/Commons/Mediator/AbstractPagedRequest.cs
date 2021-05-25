using GoLocal.Shared.Bus.Results.Pages;

namespace GoLocal.Shared.Bus.Commons.Mediator
{
    public abstract class AbstractPagedRequest<TEntity, TResponse> : AbstractRequest<Page<TResponse>>
    {
        
        public int Take { get; init; }
        public int Skip { get; init; }
        
        public Order Order { get; init; }

        internal PageRequestConfiguration<TEntity> Configuration { get; }

        protected AbstractPagedRequest()
        {
            Take = Take switch
            {
                < 10 => 10,
                > 100 => 100,
                _ => Take
            };

            Skip = Skip switch
            {
                < 0 => 0,
                _ => Skip
            };

            Configuration = new PageRequestConfiguration<TEntity>();
            
            Build();
        }

        protected abstract void ConfigurePaging(PageRequestConfiguration<TEntity> paging);
        
        private void Build()
        {
            ConfigurePaging(Configuration);
        }
    }
}