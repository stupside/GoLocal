using FluentValidation;

namespace GoLocal.Artisan.Application.Commands.Shops.DeleteShop
{
    public class DeleteShopValidator : AbstractValidator<DeleteShopCommand>
    {
        public DeleteShopValidator()
        {
            RuleFor(m => m.ShopId).NotEmpty();
            RuleFor(m => m.Name).NotEmpty();
        }
    }
}