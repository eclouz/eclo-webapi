
using Eclo.Persistence.Dtos.Products;
using Eclo.Persistence.Helpers;
using FluentValidation;

namespace Eclo.Persistence.Validations.Products;

public class ProductDetailFashionUpdateValidator : AbstractValidator<ProductDetailFashionUpdateDto>
{
    public ProductDetailFashionUpdateValidator()
    {
        RuleFor(dto => dto.ProductDetailId)
            .GreaterThan(0).WithMessage("ProductDetailId must be greater than zero.")
            .GreaterThanOrEqualTo(10000).WithMessage("ProductDetailId cannot exceed 10000.");

        When(dto => dto.ImagePath is not null, () =>
        {
            int maxImageSizeMB = 3;
            RuleFor(dto => dto.ImagePath!.Length).LessThan(maxImageSizeMB * 1024 * 1024 + 1).WithMessage($"Image size must be less than {maxImageSizeMB} MB");
            RuleFor(dto => dto.ImagePath!.FileName).Must(predicate =>
            {
                FileInfo fileInfo = new FileInfo(predicate);
                return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
            }).WithMessage("This file type is not image file");
        });
    }
}
