using FluentValidation;

namespace GoLocal.Core.Artisan.Application.Commands.Items.UpdateItemImage
{
    public class UpdateItemImageValidator : AbstractValidator<UpdateItemImageCommand>
    {
        public UpdateItemImageValidator()
        {
            RuleFor(m => m.ItemId).NotEmpty();
            RuleFor(m => m.ShopId).NotEmpty();
            RuleFor(m => m.File).NotEmpty();
        }
    }
}