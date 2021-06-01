using FluentValidation;

namespace GoLocal.Core.Artisan.Application.Commands.Invoices.MakeInvoiceReady
{
    public class MakeInvoiceReadyValidator : AbstractValidator<MakeInvoiceReadyCommand>
    {
        public MakeInvoiceReadyValidator()
        {
            RuleFor(m => m.InvoiceId).NotEmpty();
            RuleFor(m => m.ShopId).NotEmpty();
        }
    }
}