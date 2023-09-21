﻿using Eclo.Persistence.DTOs.Admins;
using Eclo.Persistence.Helpers;
using FluentValidation;

namespace Eclo.Persistence.Validations.Admins;

public class AdminCreateValidator : AbstractValidator<AdminCreateDto>
{
    public AdminCreateValidator()
    {
        RuleFor(dto => dto.FirstName)
            .NotNull().NotEmpty().WithMessage("FirstName field is required!")
            .Length(3, 20).WithMessage("FirstName must be between 3 and 20 characters.")
            .Matches("^[A-Za-z]+$").WithMessage("FirstName can only contain letters");

        RuleFor(dto => dto.LastName)
            .NotNull().NotEmpty().WithMessage("LastName field is required!")
            .Length(3, 20).WithMessage("LastName must be between 3 and 20 characters.")
            .Matches("^[A-Za-z]+$").WithMessage("LastName can only contain letters");

        RuleFor(dto => dto.BirthDate)
            .NotNull().NotEmpty().WithMessage("BirthDate field is required!")
            .Must(ValidDateOfBirth).WithMessage("BirthDate is invalid!");

        RuleFor(dto => dto.PhoneNumber)
            .Must(phone => PhoneNumberValidator.IsValid(phone)).WithMessage("Phone number is invalid! ex: +998xxYYYAABB");

        RuleFor(dto => dto.Password)
            .Must(password => PasswordValidator.IsStrongPassword(password).IsValid).WithMessage("Password is invalid!");

        RuleFor(dto => dto.PassportSerialNumber)
            .NotEmpty().NotNull().WithMessage("PassportSerialNumber is required!")
            .Length(9).WithMessage("PassportSerialNumber must only be 9 characters!")
            .Must(StartsWithTwoUppercaseLetters).WithMessage("The first two characters must be uppercase letters.")
            .Matches("^[A-Z0-9]+$").WithMessage("PassportSerialNumber can only contain capital letters and numbers.");

        int maxImageSizeMB = 3;
        RuleFor(dto => dto.ImagePath.Length)
            .LessThan(maxImageSizeMB * 1024 * 1024 + 1).WithMessage($"Image size must be less than {maxImageSizeMB} MB");
        RuleFor(dto => dto.ImagePath.FileName).Must(predicate =>
        {
            FileInfo fileInfo = new FileInfo(predicate);
            return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
        }).WithMessage("This file type is not image file");
    }
    private bool StartsWithTwoUppercaseLetters(string name)
    {
        if (string.IsNullOrEmpty(name) || name.Length != 9) return false;
        return char.IsUpper(name[0]) && char.IsUpper(name[1]);
    }

    private bool ValidDateOfBirth(DateTime birthDate)
    {
        if (birthDate > DateTime.Now) return false;
        else return true;
    }
}
