using Eclo.Persistence.Dtos.Discounts;
using FluentValidation;

namespace Eclo.Persistence.Validations.Discounts;

public class ProductDiscountCreateValidator : AbstractValidator<ProductDiscountCreateDto>
{
    public ProductDiscountCreateValidator()
    {
        RuleFor(dto => dto.ProductId)
            .NotEmpty().NotNull().WithMessage("ProductId is required!")
            .GreaterThan(0).WithMessage("ProductId must be greater than zero.")
            .GreaterThanOrEqualTo(10000).WithMessage("ProductId cannot exceed 10000.");

        RuleFor(dto => dto.DiscountId)
            .NotEmpty().NotNull().WithMessage("DiscountId is required!")
            .GreaterThan(0).WithMessage("DiscountId must be greater than zero.")
            .GreaterThanOrEqualTo(10000).WithMessage("DiscountId cannot exceed 10000.");

        RuleFor(dto => dto.Description)
            .NotNull().NotEmpty().WithMessage("Description field is required!")
            .MinimumLength(3).WithMessage("Description field is required!");

        RuleFor(dto => dto.StartAt)
            .NotNull().NotEmpty().WithMessage("StartAt field is required!")
            .Must(ValidDateOfBirth).WithMessage("StartAt is invalid!");

        RuleFor(dto => dto.EndAt)
            .NotNull().NotEmpty().WithMessage("EndAt field is required!")
            .Must(ValidDateOfBirth).WithMessage("EndAt is invalid!");
    }

    private bool ValidDateOfBirth(DateTime birthDate)
    {
        if (birthDate > DateTime.Now) return false;
        else return true;
    }
}
