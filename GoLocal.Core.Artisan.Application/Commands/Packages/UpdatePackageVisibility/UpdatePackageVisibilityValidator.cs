using FluentValidation;

namespace GoLocal.Core.Artisan.Application.Commands.Packages.UpdatePackageVisibility
{
    public class UpdatePackageVisibilityValidator : AbstractValidator<UpdatePackageVisibilityCommand>
    {
        public UpdatePackageVisibilityValidator()
        {
            RuleFor(m => m.ItemId).NotEmpty();
            RuleFor(m => m.PackageId).NotEmpty();
            RuleFor(m => m.ShopId).NotEmpty();
            RuleFor(m => m.Visibility).NotEmpty().IsInEnum();
        }
    }
}