using Eclo.Persistence.Dtos.Payments;
using FluentValidation;

namespace Eclo.Persistence.Validations.Payments;

public class CardCreateValidator : AbstractValidator<CardCreateDto>
{
    public CardCreateValidator()
    {
        RuleFor(dto => dto.CardHolderName)
            .NotEmpty().NotNull().WithMessage("CardHolderName field is required!")
            .Length(3, 100).WithMessage("CardHolderName must be between 3 and 50 characters.")
            .Must(ShouldStartWithUpper).WithMessage("CardHolderName must start with Uppercase letter.");

        RuleFor(dto => dto.CardNumber)
            .NotEmpty().NotNull().WithMessage("CardNumber field is required!")
            .Length(19).WithMessage("CardNumber must be 19 characters (with white spaces). (XXXX ZZZZ VVVV TTTT)");

        RuleFor(dto => dto.Balance)
            .NotNull().NotEmpty().WithMessage("Balance field is required!")
            .GreaterThan(0).WithMessage("Balance must be greater than zero.");

        RuleFor(dto => dto.PinCode)
            .NotNull().NotEmpty().WithMessage("PinCode field is required!")
            .GreaterThanOrEqualTo(1000).WithMessage("PinCode must be greater than or equal to 1000.")
            .LessThanOrEqualTo(9999).WithMessage("PinCode must be less than or equal to 9999.");

        RuleFor(dto => dto.ExpiredMonth)
            .NotNull().NotEmpty().WithMessage("ExpiredMonth field is required!")
            .GreaterThanOrEqualTo(1).WithMessage("ExpiredMonth must be greater than or equal to 1.")
            .LessThanOrEqualTo(12).WithMessage("ExpiredMonth must be less than or equal to 12.");

        RuleFor(dto => dto.ExpiredYear)
            .NotNull().NotEmpty().WithMessage("ExpiredYear field is required!")
            .GreaterThanOrEqualTo(2023).WithMessage("ExpiredYear must be greater than or equal to 2023.");
    }

    private bool ShouldStartWithUpper(string name)
    {
        if (string.IsNullOrEmpty(name)) return false;
        return char.IsUpper(name[0]);
    }
}
