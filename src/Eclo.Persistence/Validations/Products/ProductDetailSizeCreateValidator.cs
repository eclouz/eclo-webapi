using Eclo.Persistence.Dtos.Products;
using FluentValidation;

namespace Eclo.Persistence.Validations.Products;

public class ProductDetailSizeCreateValidator : AbstractValidator<ProductDetailSizeCreateDto>
{
    public ProductDetailSizeCreateValidator()
    {
        RuleFor(dto => dto.Size).Must(size => SizeValidator.IsValid(size))
            .WithMessage("Size is invalid! ex: XL, XXL,");
    }
}
