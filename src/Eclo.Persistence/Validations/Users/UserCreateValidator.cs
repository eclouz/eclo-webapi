using Eclo.Persistence.Dtos.Users;
using FluentValidation;

namespace Eclo.Persistence.Validations.Users;

public class UserCreateValidator : AbstractValidator<UserCreateDto>
{
    public UserCreateValidator()
    {
        RuleFor(dto => dto.Address).NotEmpty().NotNull().WithMessage("Address is required!");
            
        RuleFor(dto => dto.District).NotEmpty().NotNull().WithMessage("District is required!")
            .MinimumLength(3).WithMessage("District must be more than 3 characters!")
            .MaximumLength(50).WithMessage("District must be less than 50 characters!");

        RuleFor(dto => dto.Region).NotEmpty().NotNull().WithMessage("Region is required!")
            .MinimumLength(3).WithMessage("Region must be more than 3 characters!")
            .MaximumLength(50).WithMessage("Region must be less than 50 characters!");

        RuleFor(dto => dto.PhoneNumber).NotNull().NotEmpty().WithMessage("User phone number is required!")
            .Must(phone => PhoneNumberValidator.IsValid(phone)).WithMessage("Phone number is incorrect!");

        RuleFor(dto => dto.PassportSerialNumber).NotEmpty().NotNull().WithMessage("PassportSerialNumber is required!")
            .MinimumLength(9).WithMessage("PassportSerialNumber must be more than 9 characters!")
            .MaximumLength(9).WithMessage("PassportSerialNumber must be less than 9 characters!");
    }
}
