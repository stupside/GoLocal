namespace GoLocal.Client.Application.Queries.Shops.GetShop.Models
{
    public class LocationDto
    {
        public string Longitude { get; init; }
        public string Latitude { get; init; }
        public string Street { get; init; }
        public string Zip { get; init; }
        public string Country { get; init; }
        public string City { get; init; }
        public string Address { get; init; }
        public string Region { get; init; }
    }
}