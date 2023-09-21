using Eclo.Persistence.Dtos.Products;
using Eclo.Persistence.Validations.Products;
using Xunit;

namespace Eclo.UnitTest.ValidatorTests.Products;

public class ProductDetailSizeCreateValidatorTest
{
    [Theory]
    [InlineData(1, "X", 10)]
    [InlineData(10, "XL", 15)]
    [InlineData(111, "XXL", 20)]
    public void ShouldReturnValidValidation(long id, string size, int quantity)
    {
        ProductDetailSizeCreateDto productDetailSizeCreateDto = new ProductDetailSizeCreateDto()
        {
            ProductDetailId = id,
            Size = size,
            Quantity = quantity
        };
        var validator = new ProductDetailSizeCreateValidator();
        var result = validator.Validate(productDetailSizeCreateDto);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(0, "X#", -10)]
    [InlineData(-10, "X12", 0)]
    [InlineData(10.3, "X12f", 10.4)]
    public void ShouldReturnInvalidValidation(long id, string size, int quantity)
    {
        ProductDetailSizeCreateDto productDetailSizeCreateDto = new ProductDetailSizeCreateDto()
        {
            ProductDetailId = id,
            Size = size,
            Quantity = quantity
        };
        var validator = new ProductDetailSizeCreateValidator();
        var result = validator.Validate(productDetailSizeCreateDto);
        Assert.False(result.IsValid);
    }
}
