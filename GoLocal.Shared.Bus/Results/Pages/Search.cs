using System;
using System.Collections.Generic;

namespace GoLocal.Shared.Bus.Results.Pages
{
    public class Search<TEntity>
    {
        public IDictionary<string, object> Values { get; set; }
        private IDictionary<string, Func<TEntity, dynamic>> Maps { get; }
        
        public void MapFor(string name, Func<TEntity, dynamic> map)
            => Maps.Add(name, map);
        
        /// <summary>
        /// It will search in the values if there is an existing map configuration
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Func<TEntity, object> GetMap(string name)
        {
            if (!Maps.TryGetValue(name, out Func<TEntity, object> order))
                throw new NotImplementedException(name);
            
            return order;
        }
    }
}