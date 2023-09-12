using Eclo.Persistence.Dtos.Auth;
using FluentValidation;

namespace Eclo.Persistence.Validations.Auth;

public class VerifyRegisterValidator : AbstractValidator<VerifyRegisterDto>
{
    public VerifyRegisterValidator()
    {
        RuleFor(dto => dto.PhoneNumber).Must(phone => PhoneNumberValidator.IsValid(phone))
            .WithMessage("Phone number is invalid! ex: +998xxYYYAABB");

        RuleFor(dto => dto.Code).NotEmpty().WithMessage("Code field is required")
            .GreaterThan(10000).WithMessage("Code must be greater than 10000")
            .LessThan(99999).WithMessage("Code must be less than 99999");
    }
}
