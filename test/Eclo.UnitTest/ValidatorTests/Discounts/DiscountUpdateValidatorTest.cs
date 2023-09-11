using Eclo.Persistence.Dtos.Discounts;
using Eclo.Persistence.Validations.Discounts;
using Xunit;

namespace Eclo.UnitTest.ValidatorTests.Discounts;

public class DiscountUpdateValidatorTest
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
        DiscountUpdateDto discountUpdateDto = new DiscountUpdateDto()
        {
            Name = name,
        };
        var validator = new DiscountUpdateValidator();
        var result = validator.Validate(discountUpdateDto);
        Assert.False(result.IsValid);
    }
}
