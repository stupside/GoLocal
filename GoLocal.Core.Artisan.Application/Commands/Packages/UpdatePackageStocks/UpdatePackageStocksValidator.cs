using FluentValidation;

namespace GoLocal.Core.Artisan.Application.Commands.Packages.UpdatePackageStocks
{
    public class UpdatePackageStocksValidator : AbstractValidator<UpdatePackageStocksCommand>
    {
        public UpdatePackageStocksValidator()
        {
            RuleFor(m => m.ItemId).NotEmpty();
            RuleFor(m => m.PackageId).NotEmpty();
            RuleFor(m => m.ShopId).NotEmpty();
            RuleFor(m => m.Stocks).GreaterThanOrEqualTo(0);
        }
    }
}