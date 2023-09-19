using Eclo.Persistence.Dtos.Products;
using FluentValidation;

namespace Eclo.Persistence.Validations.Products;

public class ProductDetailSizeCreateValidator : AbstractValidator<ProductDetailSizeCreateDto>
{
    public ProductDetailSizeCreateValidator()
    {
        RuleFor(dto => dto.ProductDetailId)
            .NotEmpty().NotNull().WithMessage("ProductDetailId is required!")
            .GreaterThan(0).WithMessage("ProductDetailId must be greater than zero.")
            .GreaterThanOrEqualTo(10000).WithMessage("ProductDetailId cannot exceed 10000.");

        RuleFor(dto => dto.Size)
            .Must(size => SizeValidator.IsValid(size)).WithMessage("Size is invalid! ex: XL, XXL,")
            .Length(1,5).WithMessage("Color must be between 1 and 5 characters.")
            .Matches("^[A-Za-z]+$").WithMessage("Color can only contain letters");

        RuleFor(dto => dto.Quantity)
            .NotEmpty().NotNull().WithMessage("Quantity field is required!")
            .GreaterThanOrEqualTo(1).WithMessage("Quantity must be greater than or equal to one.")
            .LessThanOrEqualTo(50).WithMessage("Quantity must be less than or equal to fifty.");
    }
}
