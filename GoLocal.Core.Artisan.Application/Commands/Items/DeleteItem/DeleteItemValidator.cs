using FluentValidation;

namespace GoLocal.Core.Artisan.Application.Commands.Items.DeleteItem
{
    public class DeleteItemValidator : AbstractValidator<DeleteItemCommand>
    {
        public DeleteItemValidator()
        {
            RuleFor(m => m.ItemId).NotEmpty();
            RuleFor(m => m.ShopId).NotEmpty();
            RuleFor(m => m.Name).NotEmpty();
        }
    }
}