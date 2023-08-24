using Eclo.Persistence.Dtos.Users;
using Eclo.Persistence.Helpers;
using FluentValidation;

namespace Eclo.Persistence.Validations.Users;

public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateValidator()
    {
        RuleFor(dto => dto.FirstName).NotNull().NotEmpty().WithMessage("Firstname is required!")
            .MaximumLength(30).WithMessage("Firstname must be less than 30 characters");

        RuleFor(dto => dto.LastName).NotNull().NotEmpty().WithMessage("Lastname is required!")
            .MaximumLength(30).WithMessage("Lastname must be less than 30 characters");

        When(dto => dto.ImagePath is not null, () =>
        {
            int maxImageSizeMB = 5;
            RuleFor(dto => dto.ImagePath!.Length).LessThan(maxImageSizeMB * 1024 * 1024).WithMessage($"Image size must be less than {maxImageSizeMB} MB");
            RuleFor(dto => dto.ImagePath!.FileName).Must(predicate =>
            {
                FileInfo fileInfo = new FileInfo(predicate);
                return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
            }).WithMessage("This file type is not image file");
        });

        RuleFor(dto => dto.PhoneNumber).Must(phone => PhoneNumberValidator.IsValid(phone))
           .WithMessage("Phone number is invalid! ex: +998xxYYYAABB");

        RuleFor(dto => dto.PassportSerialNumber).NotEmpty().NotNull().WithMessage("PassportSerialNumber is required!")
            .MinimumLength(9).WithMessage("PassportSerialNumber must be more than 9 characters!")
            .MaximumLength(9).WithMessage("PassportSerialNumber must be less than 9 characters!");
        
        RuleFor(dto => dto.Region).NotEmpty().NotNull().WithMessage("Region is required!")
           .MinimumLength(3).WithMessage("Region must be more than 3 characters!")
           .MaximumLength(50).WithMessage("Region must be less than 50 characters!");

        RuleFor(dto => dto.District).NotEmpty().NotNull().WithMessage("District is required!")
            .MinimumLength(3).WithMessage("District must be more than 3 characters!")
            .MaximumLength(50).WithMessage("District must be less than 50 characters!");

        RuleFor(dto => dto.Address).NotEmpty().NotNull().WithMessage("Address is required!");
    }
}
