using Eclo.Persistence.Dtos.Products;
using Eclo.Persistence.Validations.Products;
using Xunit;

namespace Eclo.UnitTest.ValidatorTests.Products;

public class ProductDetailSizeUpdateValidatorTest
{
    [Theory]
    [InlineData(1, "X", 10)]
    [InlineData(10, "XL", 15)]
    [InlineData(111, "XXL", 20)]
    public void ShouldReturnValidValidation(long id, string size, int quantity)
    {
        ProductDetailSizeUpdateDto productDetailSizeUpdateDto = new ProductDetailSizeUpdateDto()
        {
            ProductDetailId = id,
            Size = size,
            Quantity = quantity
        };
        var validator = new ProductDetailSizeUpdateValidator();
        var result = validator.Validate(productDetailSizeUpdateDto);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(0, "X#", -10)]
    [InlineData(-10, "X12", 0)]
    [InlineData(10.3, "X12f", 10.4)]
    public void ShouldReturnInvalidValidation(long id, string size, int quantity)
    {
        ProductDetailSizeUpdateDto productDetailSizeUpdateDto = new ProductDetailSizeUpdateDto()
        {
            ProductDetailId = id,
            Size = size,
            Quantity = quantity
        };
        var validator = new ProductDetailSizeUpdateValidator();
        var result = validator.Validate(productDetailSizeUpdateDto);
        Assert.False(result.IsValid);
    }
}
