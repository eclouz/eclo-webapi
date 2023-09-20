using Eclo.Persistence.Dtos.Products;
using FluentValidation;

namespace Eclo.Persistence.Validations.Products;

public class ProductCreateValidator : AbstractValidator<ProductCreateDto>
{
    public ProductCreateValidator()
    {
        RuleFor(dto => dto.BrandId)
            .NotEmpty().NotNull().WithMessage("BrandId is required!")
            .GreaterThan(0).WithMessage("BrandId must be greater than zero.")
            .LessThanOrEqualTo(10000).WithMessage("BrandId cannot exceed 10000.");

        RuleFor(dto => dto.SubCategoryId)
            .NotEmpty().NotNull().WithMessage("SubCategoryId is required!")
            .GreaterThan(0).WithMessage("SubCategoryId must be greater than zero.")
            .LessThanOrEqualTo(10000).WithMessage("SubCategoryId cannot exceed 10000.");

        RuleFor(dto => dto.Name)
            .NotNull().NotEmpty().WithMessage("Name field is required!")
            .Length(3, 50).WithMessage("Name must be between 3 and 50 characters.")
            .Matches("^[A-Za-z0-9]+$").WithMessage("Name can only contain letters");

        RuleFor(dto => dto.UnitPrice)
            .NotNull().NotEmpty().WithMessage("UnitPrice field is required!")
            .GreaterThan(0).WithMessage("Percentage must be greater than zero.");

        RuleFor(dto => dto.Description)
            .NotNull().NotEmpty().WithMessage("Description field is required!")
            .MinimumLength(3).WithMessage("Description field is required!");
    }
}
