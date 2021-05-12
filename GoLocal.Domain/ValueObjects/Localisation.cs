namespace GoLocal.Domain.ValueObjects
{
    public class Localisation
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Localisation()
        {
        }

        public Localisation(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}