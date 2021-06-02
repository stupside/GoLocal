using FluentValidation;

namespace GoLocal.Core.Artisan.Application.Commands.Invoices.MakeInvoicePicked
{
    public class MakeInvoicePickedValidator : AbstractValidator<MakeInvoicePickedCommand>
    {
        public MakeInvoicePickedValidator()
        {
            RuleFor(m => m.Identifier).NotEmpty();
            RuleFor(m => m.ShopId).NotEmpty();
        }
    }
}