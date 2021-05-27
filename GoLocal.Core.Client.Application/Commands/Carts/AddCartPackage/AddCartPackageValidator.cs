using FluentValidation;

namespace GoLocal.Core.Client.Application.Commands.Carts.AddCartPackage
{
    public class AddCartPackageValidator : AbstractValidator<AddCartPackageCommand>
    {
        public AddCartPackageValidator()
        {
            RuleFor(m => m.Quantity).NotEmpty().ExclusiveBetween(0, 10);
            RuleFor(m => m.ItemId).NotEmpty();
            RuleFor(m => m.PackageId).NotEmpty();
            RuleFor(m => m.ShopId).NotEmpty();

        }
    }
}