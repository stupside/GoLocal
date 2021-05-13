using System;
using FluentValidation;
using GoLocal.Domain.ValueObjects;

namespace GoLocal.Artisan.Application.Commands.Shops.UpdateShopOpening
{
    public class UpdateShopOpeningValidator : AbstractValidator<UpdateShopOpeningCommand>
    {
        public UpdateShopOpeningValidator()
        {
            RuleFor(m => m.ShopId).NotEmpty();
            RuleFor(m => m.Day).IsInEnum();

            RuleFor(m => m.Morning).ChildRules(m =>
            {
                m.RuleFor(r => r.Max).InclusiveBetween(TimeSpan.Zero, TimeSpan.FromHours(12));
                m.RuleFor(r => r.Min).InclusiveBetween(TimeSpan.Zero, TimeSpan.FromHours(12)).LessThanOrEqualTo(r => r.Max);
            });
            
            RuleFor(m => m.Evening).ChildRules(m =>
            {
                m.RuleFor(r => r.Max).InclusiveBetween(TimeSpan.Zero, TimeSpan.FromHours(12));
                m.RuleFor(r => r.Min).InclusiveBetween(TimeSpan.Zero, TimeSpan.FromHours(12)).LessThanOrEqualTo(r => r.Max);
            });
        }
    }
}