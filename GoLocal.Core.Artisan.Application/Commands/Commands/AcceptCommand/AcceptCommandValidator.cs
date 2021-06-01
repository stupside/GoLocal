using FluentValidation;

namespace GoLocal.Core.Artisan.Application.Commands.Commands.AcceptCommand
{
    public class RejectCommandValidator : AbstractValidator<AcceptCommandCommand>
    {
        public RejectCommandValidator()
        {
            RuleFor(m => m.ShopId).NotEmpty();
            RuleFor(m => m.CommandId).NotEmpty();
        }
    }
}