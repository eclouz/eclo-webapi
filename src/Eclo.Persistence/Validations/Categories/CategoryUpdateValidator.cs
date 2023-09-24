using Eclo.Persistence.Dtos.Categories;
using FluentValidation;

namespace Eclo.Persistence.Validations.Categories;

public class CategoryUpdateValidator : AbstractValidator<CategoryUpdateDto>
{
    public CategoryUpdateValidator()
    {
        RuleFor(dto => dto.Name)
            .NotNull().NotEmpty().WithMessage("Name field is required!")
            .Length(3, 50).WithMessage("Name must be between 3 and 50 characters.")
            .Matches("^[A-Za-z\\s'-]+$").WithMessage("Name can only contain letters")
            .Must(ShouldStartWithUpper).WithMessage("Name must start with Uppercase letter.");
        //.Matches(@"[""!@$%^&*(){}:;<>,.?/+\-_=|'[\]~\\]")!.WithMessage("Name must contain one or more special characters.");

        RuleFor(dto => dto.Description)
            .NotEmpty().NotNull().WithMessage("Description field is required!")
            .MinimumLength(3).WithMessage("Description field is required!");
    }

    private bool ShouldStartWithUpper(string name)
    {
        if (string.IsNullOrEmpty(name)) return false;
        return char.IsUpper(name[0]);
    }
}
