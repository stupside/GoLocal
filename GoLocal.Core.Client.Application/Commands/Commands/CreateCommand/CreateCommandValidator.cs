using FluentValidation;

namespace GoLocal.Core.Client.Application.Commands.Commands.CreateCommand
{
    public class CreateCommandValidator : AbstractValidator<CreateCommandCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(m => m.ServiceId).NotEmpty();
            RuleFor(m => m.PackageId).NotEmpty();
            RuleFor(m => m.Specifications).NotEmpty();
            RuleFor(m => m.Price).NotEmpty().InclusiveBetween(1, 10000);
        }
    }
}