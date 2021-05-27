using FluentValidation;

namespace GoLocal.Core.Artisan.Application.Commands.Items.CreateItem
{
    public class CreateItemValidator : AbstractValidator<CreateItemCommand>
    {
        public CreateItemValidator()
        {
            RuleFor(m => m.ShopId).NotEmpty();
            RuleFor(m => m.Name).NotEmpty();
            RuleFor(m => m.Description).NotEmpty();
        }
    }
}