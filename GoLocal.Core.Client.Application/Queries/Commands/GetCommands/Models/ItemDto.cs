namespace GoLocal.Core.Client.Application.Queries.Commands.GetCommands.Models
{
    public class ItemDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public ShopDto Shop { get; init; }
    }
}