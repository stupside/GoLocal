using FluentValidation;

namespace GoLocal.Core.Artisan.Application.Commands.Items.UpdateItemDescription
{
    public class UpdateItemDescriptionValidator : AbstractValidator<UpdateItemDescriptionCommand>
    {
        public UpdateItemDescriptionValidator()
        {
            RuleFor(m => m.Description).NotEmpty();
        }
    }
}