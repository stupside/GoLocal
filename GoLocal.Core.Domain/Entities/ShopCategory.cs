namespace GoLocal.Core.Domain.Entities
{
    public class ShopCategory
    {
        public int Id { get; set; }

        public ShopCategory(){}

        public ShopCategory(Shop shop, Category category)
            : this()
        {
            ShopId = shop.Id;
            CategoryId = category.Id;
        }
        
        public int ShopId { get; }
        public virtual Shop Shop { get; }
        
        public int CategoryId { get; }
        public virtual Category Category { get; }
    }
}