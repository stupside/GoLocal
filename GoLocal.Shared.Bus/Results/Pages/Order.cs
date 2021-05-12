using System;
using System.Collections.Generic;

namespace GoLocal.Shared.Bus.Results.Pages
{
    public class Order<TEntity>
    {
        public string Name { get; init; }
        public Dir Direction { get; init; }
        private IDictionary<string, Func<TEntity, dynamic>> Maps { get; }

        public Order(string name = default, Dir direction = Dir.Ascending)
        {
            Maps = new Dictionary<string, Func<TEntity, dynamic>>();
            Name = name;
            Direction = direction;
        }
        
        public enum Dir
        {
            Ascending,
            Descending
        }

        public void MapFor(string name, Func<TEntity, dynamic> map)
            => Maps.Add(name, map);

        public Func<TEntity, object> GetMap()
        {
            if (!Maps.TryGetValue(Name, out Func<TEntity, object> order))
                throw new NotImplementedException(Name);
            
            return order;
        }
    }
}