using FluentValidation;

namespace GoLocal.Artisan.Application.Commands.Commands.GenerateCommandInvoice
{
    public class GenerateCommandInvoiceValidator : AbstractValidator<GenerateCommandInvoiceCommand>
    {
        public GenerateCommandInvoiceValidator()
        {
            RuleFor(m => m.CommandId).NotEmpty();
        }
    }
}