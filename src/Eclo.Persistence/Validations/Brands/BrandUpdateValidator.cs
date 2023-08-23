using Eclo.Persistence.Dtos.Brands;
using Eclo.Persistence.Helpers;
using FluentValidation;

namespace Eclo.Persistence.Validations.Brands;

public class BrandUpdateValidator : AbstractValidator<BrandUpdateDto>
{
    public BrandUpdateValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty().NotNull().WithMessage("Brand name is required!")
            .MinimumLength(3).WithMessage("Brand name must be more than 3 characters!")
            .MaximumLength(50).WithMessage("Brand name must be less than 50 characters!");

        When(dto => dto.BrandIconPath is not null, () =>
        {
            int maxImageSizeMB = 5;
            RuleFor(dto => dto.BrandIconPath!.Length).LessThan(maxImageSizeMB * 1024 * 1024).WithMessage($"Image size must be less than {maxImageSizeMB} MB");
            RuleFor(dto => dto.BrandIconPath!.FileName).Must(predicate =>
            {
                FileInfo fileInfo = new FileInfo(predicate);
                return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
            }).WithMessage("This file type is not image file");
        });
    }
}
