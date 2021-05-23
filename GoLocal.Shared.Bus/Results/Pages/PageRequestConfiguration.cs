using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GoLocal.Shared.Bus.Results.Pages
{
    public class PageRequestConfiguration<TEntity>
    {
        private IDictionary<string, Expression<Func<TEntity, object>>> Maps { get; set; }
        
        public void MapFor(string name, Expression<Func<TEntity, object>> map)
        {
            Maps ??= new Dictionary<string, Expression<Func<TEntity, object>>>();

            Maps.Add(name, map);
        }
        public Expression<Func<TEntity, object>> GetMap(string name)
        {
            if (Maps == null)
                throw new NotSupportedException();
            
            if (!Maps.TryGetValue(name, out Expression<Func<TEntity, object>> map))
                throw new NotSupportedException();
            
            return map;
        }
    }
}