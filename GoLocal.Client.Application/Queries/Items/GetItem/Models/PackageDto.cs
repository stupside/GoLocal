namespace GoLocal.Client.Application.Queries.Items.GetItem.Models
{
    public class PackageDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public int Stocks { get; init; }
        public float Price { get; init; }
        public bool Hidden { get; init; }
        public bool Available { get; init; }
    }
}