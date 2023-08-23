using Eclo.Persistence.Dtos.Categories;
using FluentValidation;

namespace Eclo.Persistence.Validations.Categories;

public class CategoryCreateValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateValidator()
    {
        RuleFor(dto => dto.Name).NotNull().NotEmpty().WithMessage("Name field is required!")
            .MinimumLength(3).WithMessage("Name must be more than 3 characters")
            .MaximumLength(50).WithMessage("Name must be less than 50 characters");

        RuleFor(dto => dto.Description).NotNull().NotEmpty().WithMessage("Description field is required!")
            .MinimumLength(3).WithMessage("Description field is required!");
    }
}
