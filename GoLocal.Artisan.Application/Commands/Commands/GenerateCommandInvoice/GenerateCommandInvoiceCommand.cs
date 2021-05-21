using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Artisan.Application.Commands.Commands.GenerateCommandInvoice
{
    public class GenerateCommandInvoiceCommand : AbstractRequest<int>
    {
        public string CommandId { get; init; }
    }
}