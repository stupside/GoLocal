using FluentValidation;

namespace GoLocal.Artisan.Application.Commands.Packages.CreatePackage
{
    public class CreatePackageCommandValidator : AbstractValidator<CreatePackageCommand>
    {
        public CreatePackageCommandValidator()
        {
            RuleFor(m => m.Name).NotEmpty();
            RuleFor(m => m.Price).InclusiveBetween(1, 10000);
            RuleFor(m => m.Stocks).GreaterThan(0);
            RuleFor(m => m.Description).NotEmpty();
            RuleFor(m => m.ItemId).NotEmpty();
            RuleFor(m => m.ShopId).NotEmpty();
        }
    }
}