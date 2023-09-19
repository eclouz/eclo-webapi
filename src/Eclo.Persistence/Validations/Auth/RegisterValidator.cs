using Eclo.Persistence.Dtos.Auth;
using FluentValidation;

namespace Eclo.Persistence.Validations.Auth;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(dto => dto.FirstName)
            .NotNull().NotEmpty().WithMessage("FirstName field is required!")
            .Length(3, 20).WithMessage("FirstName must be between 3 and 20 characters.")
            .Matches("^[A-Za-z]+$").WithMessage("FirstName can only contain letters");

        RuleFor(dto => dto.LastName)
            .NotNull().NotEmpty().WithMessage("LastName field is required!")
            .Length(3, 20).WithMessage("LastName must be between 3 and 20 characters.")
            .Matches("^[A-Za-z]+$").WithMessage("LastName can only contain letters");

        RuleFor(dto => dto.PhoneNumber)
            .Must(phone => PhoneNumberValidator.IsValid(phone)).WithMessage("Phone number is invalid! ex: +998xxYYYAABB");

        RuleFor(dto => dto.Password)
            .Must(password => PasswordValidator.IsStrongPassword(password).IsValid).WithMessage("Password is not strong password!");
    }
}
