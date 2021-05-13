using FluentValidation;

namespace GoLocal.Artisan.Application.Commands.Shops.UpdateShopLocalisation
{
    public class UpdateShopLocalisationValidator : AbstractValidator<UpdateShopLocalisationCommand>
    {
        public UpdateShopLocalisationValidator()
        {
            RuleFor(m => m.ShopId).NotEmpty();
            RuleFor(m => m.Address).NotEmpty();
            RuleFor(m => m.City).NotEmpty();
            RuleFor(m => m.Country).NotEmpty();
            RuleFor(m => m.Region).NotEmpty();
            RuleFor(m => m.Street).NotEmpty();
            RuleFor(m => m.Zip).NotEmpty();
        }
    }
}