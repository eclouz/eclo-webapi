using Eclo.Persistence.Dtos.Discounts;
using FluentValidation;

namespace Eclo.Persistence.Validations.Discounts;

public class DiscountCreateValidator : AbstractValidator<DiscountCreateDto>
{
    public DiscountCreateValidator()
    {
        RuleFor(dto => dto.Name)
            .NotNull().NotEmpty().WithMessage("Name field is required!")
            .Length(3, 50).WithMessage("Name must be between 3 and 50 characters.")
            .Matches("^[A-Za-z0-9]+$").WithMessage("Name can only contain letters");

        RuleFor(dto => dto.Percentage)
            .NotNull().NotEmpty().WithMessage("Percentage field is required!")
            .GreaterThanOrEqualTo(0).WithMessage("Percentage must be greater than or equal to zero.")
            .LessThanOrEqualTo(100).WithMessage("Percentage must be less than or equal to 100.");

        RuleFor(dto => dto.Description)
            .NotNull().NotEmpty().WithMessage("Description field is required!")
            .MinimumLength(3).WithMessage("Description field is required!");
    }
}
