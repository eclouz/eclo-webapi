using Eclo.Persistence.Dtos.Products;
using Eclo.Persistence.Validations.Products;
using Xunit;

namespace Eclo.UnitTest.ValidatorTests.Products;

public class ProductDetailSizeUpdateValidatorTest
{
    [Theory]
    [InlineData("X")]
    public void ShouldReturnValidValidation(string size)
    {
        ProductDetailSizeUpdateDto productDetailSizeUpdateDto = new ProductDetailSizeUpdateDto()
        {
            Size = size,
        };
        var validator = new ProductDetailSizeUpdateValidator();
        var result = validator.Validate(productDetailSizeUpdateDto);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("X#")]
    [InlineData("X12")]
    [InlineData("X12f")]
    [InlineData("X12f ")]
    [InlineData("12AS")]
    [InlineData("12123")]
    [InlineData("aasd")]
    [InlineData("aasd@")]
    [InlineData("X12SDWEFEWRWEF")]
    public void ShouldReturnInvalidValidation(string size)
    {
        ProductDetailSizeUpdateDto productDetailSizeUpdateDto = new ProductDetailSizeUpdateDto()
        {
            Size = size,
        };
        var validator = new ProductDetailSizeUpdateValidator();
        var result = validator.Validate(productDetailSizeUpdateDto);
        Assert.False(result.IsValid);
    }
}
