using FluentValidation;

namespace GoLocal.Core.Client.Application.Queries.Shops.GetShops
{
    public class GetShopsValidator : AbstractValidator<GetShopsQuery>
    {
        public GetShopsValidator()
        {
            RuleFor(m => m.Range).InclusiveBetween(0, 50);
            RuleFor(m => m.Location).NotEmpty().MinimumLength(10);
        }
    }
}