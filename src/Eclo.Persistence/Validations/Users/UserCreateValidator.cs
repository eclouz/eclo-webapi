using Eclo.Persistence.Dtos.Users;
using FluentValidation;

namespace Eclo.Persistence.Validations.Users;

public class UserCreateValidator : AbstractValidator<UserCreateDto>
{
    public UserCreateValidator()
    {
        RuleFor(dto => dto.PhoneNumber)
            .NotNull().NotEmpty().WithMessage("User phone number is required!")
            .Must(phone => PhoneNumberValidator.IsValid(phone)).WithMessage("Phone number is incorrect!");

        RuleFor(dto => dto.PassportSerialNumber)
            .Length(9).WithMessage("PassportSerialNumber must only be 9 characters!")
            .Must(StartsWithTwoUppercaseLetters).WithMessage("The first two characters must be uppercase letters.")
            .Matches("^[A-Z0-9]+$").WithMessage("PassportSerialNumber can only contain capital letters and numbers.");

        RuleFor(dto => dto.Region)
           .NotEmpty().NotNull().WithMessage("Region is required!")
           .Length(3, 50).WithMessage("Region must be between 3 and 50 characters.");

        RuleFor(dto => dto.District)
            .NotEmpty().NotNull().WithMessage("District is required!")
            .Length(3, 50).WithMessage("District must be between 3 and 50 characters.");

        RuleFor(dto => dto.Address)
            .NotEmpty().NotNull().WithMessage("Address is required!")
            .Length(3, 100).WithMessage("Address must be between 3 and 100 characters.");
    }

    private bool StartsWithTwoUppercaseLetters(string name)
    {
        if (string.IsNullOrEmpty(name) || name.Length != 9) return false;
        return char.IsUpper(name[0]) && char.IsUpper(name[1]);
    }
}
