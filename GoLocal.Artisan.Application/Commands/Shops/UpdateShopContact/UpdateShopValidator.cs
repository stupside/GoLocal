using FluentValidation;

namespace GoLocal.Artisan.Application.Commands.Shops.UpdateShopContact
{
    public class UpdateShopValidator : AbstractValidator<UpdateShopContactCommand>
    {
        public UpdateShopValidator()
        {
            RuleFor(m => m.ShopId).NotEmpty();
            RuleFor(m => m.Email).NotEmpty().EmailAddress();
            RuleFor(m => m.Phone).NotEmpty();
        }
    }
}