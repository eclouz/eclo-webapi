using Eclo.Persistence.Dtos.Discounts;
using FluentValidation;

namespace Eclo.Persistence.Validations.Discounts;

public class ProductDiscountUpdateValidator : AbstractValidator<ProductDiscountUpdateDto>
{
    public ProductDiscountUpdateValidator()
    {
        RuleFor(dto => dto.ProductId)
            .GreaterThan(0).WithMessage("ProductId must be greater than zero.")
            .GreaterThanOrEqualTo(10000).WithMessage("ProductId cannot exceed 10000.");

        RuleFor(dto => dto.DiscountId)
            .GreaterThan(0).WithMessage("DiscountId must be greater than zero.")
            .GreaterThanOrEqualTo(10000).WithMessage("DiscountId cannot exceed 10000.");

        RuleFor(dto => dto.Description)
            .MinimumLength(3).WithMessage("Description field is required!");

        RuleFor(dto => dto.StartAt)
            .Must(ValidDate).WithMessage("StartAt is invalid!");

        RuleFor(dto => dto.EndAt)
            .Must(ValidDate).WithMessage("EndAt is invalid!");
    }

    private bool ValidDate(DateTime date)
    {
        if (date > DateTime.Now) return false;
        else return true;
    }
}
