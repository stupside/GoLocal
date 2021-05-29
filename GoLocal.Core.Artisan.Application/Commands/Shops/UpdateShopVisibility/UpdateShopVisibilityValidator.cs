using FluentValidation;

namespace GoLocal.Core.Artisan.Application.Commands.Shops.UpdateShopVisibility
{
    public class UpdateShopVisibilityValidator : AbstractValidator<UpdateShopVisibilityCommand>
    {
        public UpdateShopVisibilityValidator()
        {
            RuleFor(m => m.ShopId).NotEmpty();
            RuleFor(m => m.Visibility).IsInEnum();
        }
    }
}