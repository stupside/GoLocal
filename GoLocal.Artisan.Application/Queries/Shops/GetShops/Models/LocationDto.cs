namespace GoLocal.Artisan.Application.Queries.Shops.GetShops.Models
{
    public class LocationDto
    {
        public double Longitude { get; init; }
        public double Latitude { get; init; }
        public string Street { get; init; }
        public string Zip { get; init; }
        public string Country { get; init; }
        public string City { get; init; }
        public string Address { get; init; }
        public string Region { get; init; }
    }
}