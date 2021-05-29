using GoLocal.Core.Domain.Enums;

namespace GoLocal.Core.Client.Application.Queries.Carts.GetCarts.Models
{
    public class ItemDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public Visibility Visibility { get; init; }
        public string VisibilityText => Visibility.ToString();
    }
}