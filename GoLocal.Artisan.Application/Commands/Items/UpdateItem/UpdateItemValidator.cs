using FluentValidation;

namespace GoLocal.Artisan.Application.Commands.Items.UpdateItem
{
    public class UpdateItemValidator : AbstractValidator<UpdateItemCommand>
    {
        public UpdateItemValidator()
        {
            RuleFor(m => m.ShopId).NotEmpty();
            RuleFor(m => m.ItemId).NotEmpty();
            RuleFor(m => m.NewName).NotEmpty();
            RuleFor(m => m.OldName).NotEmpty();
        }
    }
}