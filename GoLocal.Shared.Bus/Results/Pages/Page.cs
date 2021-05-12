using System;
using System.Collections.Generic;
using System.Linq;

namespace GoLocal.Shared.Bus.Results.Pages
{
    public class Page<T>
    {
        public int Filtered { get; }
        public int Total { get; }
        public IEnumerable<T> List { get; }

        public Page(IEnumerable<T> list, int total)
        {
            if(list == null)
                throw new ArgumentNullException(nameof(list));
            
            this.Filtered = list.Count();
            this.List = list;
            this.Total = total;
        }
    }
}