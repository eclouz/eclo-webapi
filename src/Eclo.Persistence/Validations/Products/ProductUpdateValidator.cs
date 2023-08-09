using Eclo.Persistence.Dtos.Products;
using FluentValidation;

namespace Eclo.Persistence.Validations.Products;

public class ProductUpdateValidator : AbstractValidator<ProductUpdateDto>
{
    public ProductUpdateValidator()
    {
        RuleFor(dto => dto.Name).NotNull().NotEmpty().WithMessage("Product name field is required!")
            .MinimumLength(3).WithMessage("Product name must be more than 3 characters")
            .MaximumLength(50).WithMessage("Product name must be less than 50 characters");

        RuleFor(dto => dto.Description).NotNull().NotEmpty().WithMessage("Description field is required!")
            .MinimumLength(10).WithMessage("Description field is required!");

    }
}
