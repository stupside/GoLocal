using GoLocal.Core.Domain.Enums;

namespace GoLocal.Core.Client.Application.Queries.Commands.GetCommands.Models
{
    public class CommandDto
    {
        public string Id { get; init; }
        public CommandStatus Status { get; init; }
        public string StatusText => Status.ToString();
        public PackageDto Package { get; init; }
        public InvoiceDto Invoice { get; init; }
    }
}