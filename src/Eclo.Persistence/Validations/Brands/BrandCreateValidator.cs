using Eclo.Persistence.Dtos.Brands;
using Eclo.Persistence.Helpers;
using FluentValidation;
namespace Eclo.Persistence.Validations.Brands;

public class BrandCreateValidator : AbstractValidator<BrandCreateDto>
{
    public BrandCreateValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty().NotNull().WithMessage("Name is required!")
            .Length(3, 50).WithMessage("Name must be between 3 and 50 characters.")
            .Matches("^[A-Za-z0-9]+$").WithMessage("Name can only contain letters")
            .Matches(@"[""!@$%^&*(){}:;<>,.?/+\-_=|'[\]~\\]").WithMessage("Name must contain one or more special characters.");

        int maxImageSizeMB = 3;
        RuleFor(dto => dto.BrandIconPath).NotEmpty().NotNull().WithMessage("Image field is required");
        RuleFor(dto => dto.BrandIconPath.Length).LessThan(maxImageSizeMB * 1024 * 1024 + 1).WithMessage($"Image size must be less than {maxImageSizeMB} MB");
        RuleFor(dto => dto.BrandIconPath.FileName).Must(predicate =>
        {
            FileInfo fileInfo = new FileInfo(predicate);
            return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
        }).WithMessage("This file type is not image file");
    }
}
