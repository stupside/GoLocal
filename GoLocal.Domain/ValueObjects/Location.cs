namespace GoLocal.Domain.ValueObjects
{
    public class Location
    {
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string NeighborHood { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        
        public Location(){}
    }
}