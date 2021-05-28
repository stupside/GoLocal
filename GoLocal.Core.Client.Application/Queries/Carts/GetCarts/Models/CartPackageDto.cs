namespace GoLocal.Core.Client.Application.Queries.Carts.GetCarts.Models
{
    public class CartPackageDto
    {
        public int Quantity { get; set; }
        public float Price { get; set; }

        public bool ValidPrice => Price - Package.Price == 0;
        public bool ValidQuantity => Quantity <= Package.Stocks && Package.AsStocks;
        
        public PackageDto Package { get; init; }
    }
}