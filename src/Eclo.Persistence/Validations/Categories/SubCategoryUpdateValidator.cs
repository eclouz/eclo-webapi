using Eclo.Persistence.Dtos.Categories;
using FluentValidation;

namespace Eclo.Persistence.Validations.Categories;

public class SubCategoryUpdateValidator : AbstractValidator<SubCategoryUpdateDto>
{
    public SubCategoryUpdateValidator()
    {
        RuleFor(dto => dto.CategoryId)
            .GreaterThan(0).WithMessage("CategoryId must be greater than zero.")
            .LessThanOrEqualTo(10000).WithMessage("CategoryId cannot exceed 10000.");

        RuleFor(dto => dto.Name)
            .Length(3, 50).WithMessage("Name must be between 3 and 50 characters.")
            .Matches("^[A-Za-z0-9]+$").WithMessage("Name can only contain letters");
    }
}
