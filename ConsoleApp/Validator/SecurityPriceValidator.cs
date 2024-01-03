using AOMTEST.Entity;
using FluentValidation;

namespace AOMTEST.Validator
{
    public class SecurityPriceValidator : AbstractValidator<PriceData>
    {
        public SecurityPriceValidator() 
        {
            RuleFor(x => x.ISIN)
             .NotEmpty()
             .WithMessage("Not empty");

            RuleFor(x => x.ISIN).
                Must(x => x.Length >= 12).
                WithMessage("Invalid code");

        }
    }
}
