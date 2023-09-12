﻿using Eclo.Persistence.DTOs.Admins;
using Eclo.Persistence.Helpers;
using FluentValidation;

namespace Eclo.Persistence.Validations.Admins;

public class AdminCreateValidator : AbstractValidator<AdminCreateDto>
{
    public AdminCreateValidator()
    {
        RuleFor(dto => dto.FirstName).NotNull().NotEmpty().WithMessage("FirstName field is required!")
            .MinimumLength(3).WithMessage("FirstName must be more than 3 characters")
            .MaximumLength(20).WithMessage("FirstName must be less than 20 characters");

        RuleFor(dto => dto.LastName).NotNull().NotEmpty().WithMessage("LastName field is required!")
            .MinimumLength(3).WithMessage("LastName must be more than 3 characters")
            .MaximumLength(20).WithMessage("LastName must be less than 20 characters");

        RuleFor(dto => dto.PhoneNumber).Must(phone => PhoneNumberValidator.IsValid(phone))
            .WithMessage("Phone number is invalid! ex: +998xxYYYAABB");

        RuleFor(dto => dto.Password).Must(password => PasswordValidator.IsStrongPassword(password).IsValid)
            .WithMessage("Password is invalid !");

        RuleFor(dto => dto.PassportSerialNumber).NotEmpty().NotNull().WithMessage("PassportSerialNumber is required!")
            .MinimumLength(9).WithMessage("PassportSerialNumber must be more than 9 characters!")
            .MaximumLength(9).WithMessage("PassportSerialNumber must be less than 9 characters!");

        int maxImageSizeMB = 3;
        RuleFor(dto => dto.ImagePath.Length).LessThan(maxImageSizeMB * 1024 * 1024 + 1)
            .WithMessage($"Image size must be less than {maxImageSizeMB} MB");

        RuleFor(dto => dto.ImagePath.FileName).Must(predicate =>
        {
            FileInfo fileInfo = new FileInfo(predicate);
            return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
        }).WithMessage("This file type is not image file");

        RuleFor(dto => dto.Region).NotNull().NotEmpty().WithMessage("Region field is required!")
            .MinimumLength(3).WithMessage("Region must be more than 3 characters");

        RuleFor(dto => dto.District).NotNull().NotEmpty().WithMessage("Region field is required!")
            .MinimumLength(3).WithMessage("Region must be more than 3 characters");

        RuleFor(dto => dto.Address).NotNull().NotEmpty().WithMessage("Region field is required!")
            .MinimumLength(3).WithMessage("Region must be more than 3 characters");
    }
}