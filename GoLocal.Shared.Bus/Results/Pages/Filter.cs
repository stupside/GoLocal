using FluentValidation;
using GoLocal.Shared.Bus.Commons.Filtering;

namespace GoLocal.Shared.Bus.Results.Pages
{
    
    public class Filter<TEntity> : IFilter<TEntity>
    {
        
        public int Take { get; }
        public int Skip { get; }
        public Search<TEntity> Search { get; }
        public Order<TEntity> Order { get; }
        public class FilterValidator : AbstractValidator<Filter<TEntity>>
        {
            public FilterValidator()
            {
                RuleFor(m => m.Take).InclusiveBetween(10, 100);
                RuleFor(m => m.Skip).InclusiveBetween(0, int.MaxValue);
            }
        }
    }
}