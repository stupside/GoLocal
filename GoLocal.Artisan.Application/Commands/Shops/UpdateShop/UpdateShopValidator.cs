using FluentValidation;

namespace GoLocal.Artisan.Application.Commands.Shops.UpdateShop
{
    public class UpdateShopValidator : AbstractValidator<UpdateShopCommand>
    {
        public UpdateShopValidator()
        {
            RuleFor(m => m.ShopId).NotEmpty();
            RuleFor(m => m.OldName).NotEmpty();
            RuleFor(m => m.NewName).NotEmpty();
        }
    }
}