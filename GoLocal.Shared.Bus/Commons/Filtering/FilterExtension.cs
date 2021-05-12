using System.Collections.Generic;
using System.Linq;
using GoLocal.Shared.Bus.Results.Pages;

namespace GoLocal.Shared.Bus.Commons.Filtering
{
    public static class FilterExtension
    {
        public static IEnumerable<TEntity> ComputeOrder<TEntity>(this IEnumerable<TEntity> query, IFilter<TEntity> filter)
        {
            return filter.Order.Direction == Order<TEntity>.Dir.Ascending 
                ? query.OrderBy(filter.Order.GetMap())
                : query.OrderByDescending(filter.Order.GetMap());
        }

        public static IQueryable<TEntity> ComputeLimit<TEntity>(this IQueryable<TEntity> query, IFilter<TEntity> filter)
            => query.Skip(filter.Skip).Take(filter.Take);

        public static IQueryable<TEntity> ComputeSearch<TEntity>(this IQueryable<TEntity> query,
            IFilter<TEntity> filter)
        {

            foreach (var (key, value) in filter.Search.Values)
                query = query.Where(m => filter.Search.GetMap(key).Invoke(m)
                    .ToString().Contains(value.ToString()));

            return query;
        }
    }
}