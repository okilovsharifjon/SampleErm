using FluentValidation;

namespace Erm.BusinessLayer;

public sealed class RiskProfileInfoValidator : AbstractValidator<RiskProfileDto>
{
    public RiskProfileInfoValidator()
    {
        RuleFor(prop => prop.Name).NotEmpty().MinimumLength(3).MaximumLength(50);
        RuleFor(prop => prop.Description).NotEmpty().MinimumLength(15).MaximumLength(500);
        RuleFor(prop => prop.BusinessProcessId).NotEmpty();
        RuleFor(prop => prop.OccurrenceProbability).GreaterThanOrEqualTo(1).LessThanOrEqualTo(10);
        RuleFor(prop => prop.PotentialBusinessImpact).GreaterThanOrEqualTo(1).LessThanOrEqualTo(10);
    }
}