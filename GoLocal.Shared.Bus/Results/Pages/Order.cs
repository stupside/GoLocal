namespace GoLocal.Shared.Bus.Results.Pages
{
    public class Order
    {
        public string Name { get; init; }
        public Dir Direction { get; init; }

        public Order() {}

        public Order(string name, Dir direction = Dir.Ascending)
        {
            Name = name;
            Direction = direction;
        }

        public enum Dir
        {
            Ascending,
            Descending
        }
    }
}