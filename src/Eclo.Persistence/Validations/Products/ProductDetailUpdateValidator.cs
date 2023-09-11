using Eclo.Persistence.Dtos.Products;
using Eclo.Persistence.Helpers;
using FluentValidation;

namespace Eclo.Persistence.Validations.Products;

public class ProductDetailUpdateValidator : AbstractValidator<ProductDetailUpdateDto>
{
    public ProductDetailUpdateValidator()
    {
        RuleFor(dto => dto.Color).NotEmpty().NotNull().WithMessage("Color is required!")
            .MinimumLength(3).WithMessage("Color must be more than 3 characters!")
            .MaximumLength(50).WithMessage("Color must be less than 50 characters!");

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
