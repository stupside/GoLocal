using FluentValidation;

namespace GoLocal.Core.Client.Application.Commands.Carts.GenerateCartInvoice
{
    public class GenerateCartInvoiceValidator : AbstractValidator<GenerateCartInvoiceCommand>
    {
        public GenerateCartInvoiceValidator()
        {
            RuleFor(m => m.ShopId).NotEmpty();
        }
    }
}