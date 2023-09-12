using Eclo.Persistence.Dtos.Products;
using Eclo.Persistence.Validations.Products;
using Xunit;

namespace Eclo.UnitTest.ValidatorTests.Products;

public class ProductDetailSizeCreateValidatorTest
{
    [Theory]
    [InlineData("X")]
    public void ShouldReturnValidValidation(string size)
    {
        ProductDetailSizeCreateDto productDetailSizeCreateDto = new ProductDetailSizeCreateDto()
        {
            Size = size,
        };
        var validator = new ProductDetailSizeCreateValidator();
        var result = validator.Validate(productDetailSizeCreateDto);
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
        ProductDetailSizeCreateDto productDetailSizeCreateDto = new ProductDetailSizeCreateDto()
        {
            Size = size,
        };
        var validator = new ProductDetailSizeCreateValidator();
        var result = validator.Validate(productDetailSizeCreateDto);
        Assert.False(result.IsValid);
    }
}
