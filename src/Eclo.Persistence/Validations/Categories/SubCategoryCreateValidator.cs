using Eclo.Persistence.Dtos.Categories;
using FluentValidation;

namespace Eclo.Persistence.Validations.Categories;

public class SubCategoryCreateValidator : AbstractValidator<SubCategoryCreateDto>
{
    public SubCategoryCreateValidator()
    {
        RuleFor(dto => dto.CategoryId)
            .NotEmpty().NotNull().WithMessage("CategoryId is required!")
            .GreaterThan(0).WithMessage("CategoryId must be greater than zero.")
            .LessThanOrEqualTo(10000).WithMessage("CategoryId cannot exceed 10000.");

        RuleFor(dto => dto.Name)
            .NotEmpty().NotNull().WithMessage("Name field is required!")
            .Length(3, 50).WithMessage("Name must be between 3 and 50 characters.")
            .Matches("^[A-Za-z0-9]+$").WithMessage("Name can only contain letters");
    }
}
