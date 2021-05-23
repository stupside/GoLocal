using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results.Pages;

namespace GoLocal.Shared.Bus.Commons.Filtering
{
    public static class FilterExtension
    {
        public static IEnumerable<TEntity> ApplyOrder<TEntity, TResponse>(this IEnumerable<TEntity> query, AbstractPagedRequest<TEntity, TResponse> request)
        {
            var selector = request.Configuration.GetMap(request.Order.Name).Compile();

            return request.Order.Direction == Order.Dir.Ascending 
                ? query.OrderBy(selector)
                : query.OrderByDescending(selector);
        }

        public static IQueryable<TEntity> ApplyLimit<TEntity, TResponse>(this IQueryable<TEntity> query, AbstractPagedRequest<TEntity, TResponse> request)
            => query.Skip(request.Skip).Take(request.Take);

        public static IQueryable<TEntity> ApplySearch<TEntity, TResponse>(this IQueryable<TEntity> query, AbstractPagedRequest<TEntity, TResponse> request)
            where TEntity : new()
        {
            foreach (var (key, value) in request.Search)
            {
                try
                {
                    var map = request.Configuration.GetMap(key);

                    var body = map.Body;
                    ParameterExpression parameter = map.Parameters[0];
                    
                    var lambda = Expression.Lambda<Func<TEntity, bool>>(
                        Expression.Equal((UnaryExpression)body,Expression.Constant(value)), 
                        parameter);

                    query = query.Where(lambda);
                }
                catch (NotSupportedException)
                {
                }
            }

            return query;
        }
    }
}