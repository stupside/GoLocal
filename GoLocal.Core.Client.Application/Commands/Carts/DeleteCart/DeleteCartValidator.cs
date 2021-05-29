using FluentValidation;

namespace GoLocal.Core.Client.Application.Commands.Carts.DeleteCart
{
    public class DeleteCartValidator : AbstractValidator<DeleteCartCommand>
    {
        public DeleteCartValidator()
        {
            RuleFor(m => m.ShopId).NotEmpty();
        }
    }
}