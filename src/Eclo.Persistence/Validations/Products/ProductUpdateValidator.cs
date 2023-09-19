using Eclo.Persistence.Dtos.Products;
using FluentValidation;

namespace Eclo.Persistence.Validations.Products;

public class ProductUpdateValidator : AbstractValidator<ProductUpdateDto>
{
    public ProductUpdateValidator()
    {
        RuleFor(dto => dto.BrandId)
            .GreaterThan(0).WithMessage("BrandId must be greater than zero.")
            .GreaterThanOrEqualTo(10000).WithMessage("BrandId cannot exceed 10000.");

        RuleFor(dto => dto.SubCategoryId)
            .GreaterThan(0).WithMessage("SubCategoryId must be greater than zero.")
            .GreaterThanOrEqualTo(10000).WithMessage("SubCategoryId cannot exceed 10000.");

        RuleFor(dto => dto.Name)
            .Length(3, 50).WithMessage("Name must be between 3 and 50 characters.")
            .Matches("^[A-Za-z0-9]+$").WithMessage("Name can only contain letters and numbers");

        RuleFor(dto => dto.UnitPrice)
            .GreaterThanOrEqualTo(0.0).WithMessage("Percentage must be greater than or equal to zero.");

        RuleFor(dto => dto.Description)
            .MinimumLength(3).WithMessage("Description field is required!");
    }
}
