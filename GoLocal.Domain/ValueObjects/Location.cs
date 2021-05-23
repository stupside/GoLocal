namespace GoLocal.Domain.ValueObjects
{
    public class Location
    {
        public double X { get; set; }
        public double Y { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Region { get; set; }
        
        public Location(){}
    }
}