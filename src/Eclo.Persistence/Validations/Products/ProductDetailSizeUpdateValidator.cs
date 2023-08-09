using Eclo.Persistence.Dtos.Products;
using FluentValidation;

namespace Eclo.Persistence.Validations.Products;

public class ProductDetailSizeUpdateValidator : AbstractValidator<ProductDetailSizeCreateDto>
{
    public ProductDetailSizeUpdateValidator()
    {
        RuleFor(dto => dto.Size).Must(size => SizeValidator.IsValid(size))
            .WithMessage("Size is invalid! ex: XL, XXL or 40, 120");
    }
}
