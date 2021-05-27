using FluentValidation;

namespace GoLocal.Core.Client.Application.Commands.Carts.RemoveCartPackage
{
    public class RemoveCartPackageValidator : AbstractValidator<RemoveCartPackageCommand>
    {
        public RemoveCartPackageValidator()
        {
            RuleFor(m => m.ShopId).NotEmpty();
            RuleFor(m => m.PackageId).NotEmpty();
            RuleFor(m => m.Quantity).InclusiveBetween(1, 10).NotEmpty();
        }
    }
}