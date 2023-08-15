using Eclo.Persistence.Dtos.Categories;
using Eclo.Persistence.Dtos.Discounts;
using Eclo.Persistence.Validations.Categories;
using Eclo.Persistence.Validations.Discounts;
using FluentValidation;
using Xunit;

namespace Eclo.UnitTest.ValidatorTests.Discounts;

public class DiscauntCreateValidatorTest
{
    [Theory]
    [InlineData("AA")]
    [InlineData("001")]
    [InlineData("05")]
    [InlineData("50")]
    [InlineData("Ad5d")]
    [InlineData("QE45dsfaAd45")]
    [InlineData("A")]
    [InlineData("A514-A")]
    [InlineData("/_fcdAA")]
    [InlineData("AA_=")]
    [InlineData("electronic products, we sell an electronic products to our clients, we sell an electronic products to our clients")]
    public void ShouldReturnInValidValidation(string name)
    {
        DiscountCreateDto discountCreateDto = new DiscountCreateDto()
        {
            Name = name,
        };
        var validator = new DiscountCreateValidator();
        var result = validator.Validate(discountCreateDto);
        Assert.False(result.IsValid);
    }
}
