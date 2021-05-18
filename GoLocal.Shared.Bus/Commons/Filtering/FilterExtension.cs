using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            IFilter<TEntity> filter) where TEntity : new()
        {

            Type type = typeof(TEntity);

            foreach (var (key, value) in filter.Search.Values)
            {
                TEntity test = new TEntity();
                var t = filter.Search.GetMap(key)(test).ToString();
                
                query = query.Where(m => filter.Search.GetMap(key)(m).ToString() == value); // TODO: Fix
            }

            return query;
        }
    }
}