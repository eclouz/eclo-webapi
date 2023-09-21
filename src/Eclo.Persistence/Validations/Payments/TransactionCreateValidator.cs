using Eclo.Persistence.Dtos.Payments;
using FluentValidation;

namespace Eclo.Persistence.Validations.Payments;

public class TransactionCreateValidator : AbstractValidator<TransactionCreateDto>
{
    public TransactionCreateValidator()
    {
        RuleFor(dto => dto.SenderCardNumber)
            .NotEmpty().NotNull().WithMessage("Сard number field is required!")
            .Length(19).WithMessage("Card number must be 19 characters.")
            .WithMessage("Card number must contain only numbers and white spaces between them. Card number: '1234 5678 1234 5678'");
    }
}
