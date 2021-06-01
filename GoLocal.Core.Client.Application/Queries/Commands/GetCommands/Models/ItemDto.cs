namespace GoLocal.Core.Client.Application.Queries.Commands.GetCommands.Models
{
    public class ItemDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public PackageDto Package { get; init; }
    }
}