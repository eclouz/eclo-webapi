using Eclo.Persistence.Dtos.Products;
using Eclo.Persistence.Helpers;
using FluentValidation;

namespace Eclo.Persistence.Validations.Products;

public class ProductDetailUpdateValidator : AbstractValidator<ProductDetailUpdateDto>
{
    public ProductDetailUpdateValidator()
    {
        RuleFor(dto => dto.ProductId)
            .NotEmpty().NotNull().WithMessage("ProductId is required!")
            .GreaterThan(0).WithMessage("ProductId must be greater than zero.")
            .LessThanOrEqualTo(10000).WithMessage("ProductId cannot exceed 10000.");

        RuleFor(dto => dto.Color)
            .NotEmpty().NotNull().WithMessage("Color is required!")
            .Length(3, 50).WithMessage("Color must be between 3 and 50 characters.")
            .Matches("^[A-Za-z\\s'-]+$").WithMessage("Color can only contain letters")
            .Must(ShouldStartWithUpper).WithMessage("Color must start with Uppercase letter.");

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

    private bool ShouldStartWithUpper(string name)
    {
        if (string.IsNullOrEmpty(name)) return false;
        return char.IsUpper(name[0]);
    }
}
