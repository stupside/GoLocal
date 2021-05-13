namespace GoLocal.Domain.Entities
{
    public class CartPackage
    {
        public string Id { get; set; }
        
        public int Quantity { get; set; }
        public float Price { get; set; }
        
        public CartPackage(){}
        
        public CartPackage(Cart cart, Package package, float price, int quantity = 1)
            : this()
        {
            CartId = cart.Id;
            PackageId = package.Id;

            Price = price;
            Quantity = quantity;
        }
        
        public string CartId { get; }
        
        public int PackageId { get; }
        public virtual Package Package { get; }
    }
}