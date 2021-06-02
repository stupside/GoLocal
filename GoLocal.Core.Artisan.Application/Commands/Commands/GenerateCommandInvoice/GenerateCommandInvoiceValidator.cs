using FluentValidation;

namespace GoLocal.Core.Artisan.Application.Commands.Commands.GenerateCommandInvoice
{
    public class GenerateCommandInvoiceValidator : AbstractValidator<GenerateCommandInvoiceCommand>
    {
        public GenerateCommandInvoiceValidator()
        {
            RuleFor(m => m.ShopId).NotEmpty();
            RuleFor(m => m.CommandId).NotEmpty();
        }
    }
}