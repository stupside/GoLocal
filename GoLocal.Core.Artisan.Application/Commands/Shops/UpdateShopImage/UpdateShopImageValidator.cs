using FluentValidation;

namespace GoLocal.Core.Artisan.Application.Commands.Shops.UpdateShopImage
{
    public class UpdateShopImageValidator : AbstractValidator<UpdateShopImageCommand>
    {
        public UpdateShopImageValidator()
        {
            RuleFor(m => m.File).NotEmpty();
            RuleFor(m => m.ShopId).NotEmpty();
        }
    }
}