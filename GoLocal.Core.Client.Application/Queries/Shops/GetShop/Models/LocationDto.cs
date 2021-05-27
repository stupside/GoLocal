namespace GoLocal.Core.Client.Application.Queries.Shops.GetShop.Models
{
    public class LocationDto
    {
        public string PostCode { get; init; }
        public string Country { get; init; }
        public string Region { get; init; }
        public string City { get; init; }
        public string NeighborHood { get; init; }
        public string Street { get; init; }
        public string Address { get; init; }
        public double Longitude { get; init; }
        public double Latitude { get; init; }
    }
}