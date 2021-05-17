using FluentValidation;

namespace GoLocal.Client.Application.Commands.Commands.CreateCommand
{
    public class CreateCommandValidator : AbstractValidator<CreateCommandCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(m => m.ServiceId).NotEmpty();
            RuleFor(m => m.PackageId).NotEmpty();
            RuleFor(m => m.Specifications).NotEmpty();
        }
    }
}