namespace GoLocal.Core.Client.Application.Queries.Items.GetItem.Models
{
    public class PackageDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public int Stocks { get; init; }
        public bool AsStocks => Stocks > 0;
        public float Price { get; init; }
    }
}