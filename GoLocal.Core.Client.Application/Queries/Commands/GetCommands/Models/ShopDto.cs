using GoLocal.Core.Domain.Enums;

namespace GoLocal.Core.Client.Application.Queries.Commands.GetCommands.Models
{
    public class ShopDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public Visibility Visibility { get; init; }
    }
}