using Eclo.Persistence.Dtos.Brands;
using Eclo.Persistence.Helpers;
using FluentValidation;

namespace Eclo.Persistence.Validations.Brands;

public class BrandUpdateValidator : AbstractValidator<BrandUpdateDto>
{
    public BrandUpdateValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 50).WithMessage("Name must be between 3 and 50 characters.")
            .Matches("^[A-Za-z0-9]+$").WithMessage("Name can only contain letters")
            .Matches(@"[""!@$%^&*(){}:;<>,.?/+\-_=|'[\]~\\]").WithMessage("Name must contain one or more special characters.");

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
