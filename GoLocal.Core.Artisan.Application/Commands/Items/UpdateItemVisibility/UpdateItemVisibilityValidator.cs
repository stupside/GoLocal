using FluentValidation;

namespace GoLocal.Core.Artisan.Application.Commands.Items.UpdateItemVisibility
{
    public class UpdateItemVisibilityValidator : AbstractValidator<UpdateItemVisibilityCommand>
    {
        public UpdateItemVisibilityValidator()
        {
            RuleFor(m => m.ItemId).NotEmpty();
            RuleFor(m => m.ShopId).NotEmpty();
            RuleFor(m => m.Visibility).IsInEnum();
        }
    }
}