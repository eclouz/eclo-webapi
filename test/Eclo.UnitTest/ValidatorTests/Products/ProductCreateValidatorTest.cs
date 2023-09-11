using Eclo.Persistence.Dtos.Products;
using Eclo.Persistence.Validations.Products;
using Xunit;

namespace Eclo.UnitTest.ValidatorTests.Products;

public class ProductCreateValidatorTest
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
        ProductCreateDto productCreateDto = new ProductCreateDto()
        {
            Name = name,
        };
        var validator = new ProductCreateValidator();
        var result = validator.Validate(productCreateDto);
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("Cap", "qwertyop")]
    [InlineData("Jeans", "Qqwerty12#")]
    [InlineData("Jackets", "12qwerty")]
    public void ShouldReturnValidValidation(string name, string description)
    {
        ProductCreateDto productCreateDto = new ProductCreateDto()
        {
            Name = name,
            Description = description
        };
        var validator = new ProductCreateValidator();
        var result = validator.Validate(productCreateDto);
        Assert.True(result.IsValid);
    }
}
