using Eclo.Persistence.Dtos.Products;
using Eclo.Services.Helpers;
using FluentValidation;

namespace Eclo.Persistence.Validations.Products;

public class ProductDetailFashionCreateValidator : AbstractValidator<ProductDetailFashionCreateDto>
{
    public ProductDetailFashionCreateValidator()
    {
        int maxImageSizeMB = 5;
        RuleFor(dto => dto.ImagePath).NotEmpty().NotNull().WithMessage("Image field is required");
        RuleFor(dto => dto.ImagePath.Length).LessThan(maxImageSizeMB * 1024 * 1024).WithMessage($"Image size must be less than {maxImageSizeMB} MB");
        RuleFor(dto => dto.ImagePath.FileName).Must(predicate =>
        {
            FileInfo fileInfo = new FileInfo(predicate);
            return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
        }).WithMessage("This file type is not image file");
    }
}
