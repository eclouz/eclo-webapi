using Eclo.Persistence.Dtos.Products;
using Eclo.Persistence.Helpers;
using FluentValidation;

namespace Eclo.Persistence.Validations.Products;

public class ProductDetailFashionCreateValidator : AbstractValidator<ProductDetailFashionCreateDto>
{
    public ProductDetailFashionCreateValidator()
    {
        RuleFor(dto => dto.ProductDetailId)
            .NotEmpty().NotNull().WithMessage("ProductDetailId is required!")
            .GreaterThan(0).WithMessage("ProductDetailId must be greater than zero.")
            .LessThanOrEqualTo(10000).WithMessage("ProductDetailId cannot exceed 10000.");

        int maxImageSizeMB = 3;
        RuleFor(dto => dto.ImagePath).NotEmpty().NotNull().WithMessage("Image field is required");
        RuleFor(dto => dto.ImagePath.Length).LessThan(maxImageSizeMB * 1024 * 1024 + 1).WithMessage($"Image size must be less than {maxImageSizeMB} MB");
        RuleFor(dto => dto.ImagePath.FileName).Must(predicate =>
        {
            FileInfo fileInfo = new FileInfo(predicate);
            return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
        }).WithMessage("This file type is not image file");
    }
}
