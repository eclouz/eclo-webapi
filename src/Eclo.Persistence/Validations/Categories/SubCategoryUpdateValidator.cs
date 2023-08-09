using Eclo.Persistence.Dtos.Categories;
using FluentValidation;

namespace Eclo.Persistence.Validations.Categories;

public class SubCategoryUpdateValidator : AbstractValidator<SubCategoryUpdateDto>
{
    public SubCategoryUpdateValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty().NotNull().WithMessage("SubCategory name is required!")
                .MinimumLength(3).WithMessage("SubCategory name must be more than 3 characters!")
                .MaximumLength(50).WithMessage("SubCategory name must be less than 50 characters!");
    }
}
