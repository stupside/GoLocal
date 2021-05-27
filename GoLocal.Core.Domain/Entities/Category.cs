using System.Collections.Generic;

namespace GoLocal.Core.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; }

        public Category(){}

        public Category(string title)
            : this()
        {
            Title = title;
        }

        public virtual ICollection<ShopCategory> ShopCategories { get; }
    }
}