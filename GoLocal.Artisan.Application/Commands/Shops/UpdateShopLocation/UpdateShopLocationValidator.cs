using FluentValidation;

namespace GoLocal.Artisan.Application.Commands.Shops.UpdateShopLocation
{
    public class UpdateShopLocationValidator : AbstractValidator<UpdateShopLocationCommand>
    {
        public UpdateShopLocationValidator()
        {
            RuleFor(m => m.ShopId).NotEmpty();
            RuleFor(m => m.Address).NotEmpty();
            RuleFor(m => m.City).NotEmpty();
            RuleFor(m => m.Country).NotEmpty();
            RuleFor(m => m.Region).NotEmpty();
            RuleFor(m => m.Street).NotEmpty();
            RuleFor(m => m.PostCode).NotEmpty();
        }
    }
}