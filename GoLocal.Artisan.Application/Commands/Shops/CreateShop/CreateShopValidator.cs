using FluentValidation;

namespace GoLocal.Artisan.Application.Commands.Shops.CreateShop
{
    public class CreateShopValidator : AbstractValidator<CreateShopCommand>
    {
        public CreateShopValidator()
        {
            RuleFor(m => m.Name).NotEmpty();
            RuleFor(m => m.Contact).ChildRules(m =>
            {
                m.RuleFor(r => r.Email).NotEmpty().EmailAddress();
                m.RuleFor(r => r.Phone).NotEmpty();
            });
            RuleFor(m => m.Location).ChildRules(m =>
            {
                m.RuleFor(r => r.Address).NotEmpty();
                m.RuleFor(r => r.City).NotEmpty();
                m.RuleFor(r => r.Country).NotEmpty();
                m.RuleFor(r => r.Region).NotEmpty();
                m.RuleFor(r => r.Street).NotEmpty();
                m.RuleFor(r => r.PostCode).NotEmpty();
            });
        }
    }
}