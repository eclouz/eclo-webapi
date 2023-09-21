using Eclo.Persistence.Dtos.Products;
using Eclo.Persistence.Validations.Products;
using Xunit;

namespace Eclo.UnitTest.ValidatorTests.Products;

public class ProductUpdateValidatorTest
{
    [Theory]
    [InlineData(0,0,"AA",0," ")]
    [InlineData(-10,-10,"001",-10.0," ")]
    [InlineData(0.3,0.3,"05",-3.32523," ")]
    [InlineData(-0.3,-0.3,"electronic products, we sell an electronic products to our clients, we sell an electronic products to our clients",-0," ")]
    public void ShouldReturnInValidValidation(long brandId, long subcategoryId, string name, double price, string desc)
    {
        ProductUpdateDto productUpdateDto = new ProductUpdateDto()
        {
            BrandId = brandId,
            SubCategoryId = subcategoryId,
            Name = name,
            UnitPrice = price,
            Description = desc
        };
        var validator = new ProductUpdateValidator();
        var result = validator.Validate(productUpdateDto);
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData(1,1,"Cap", 10.89,"Qwertyuiop")]
    [InlineData(123,123,"Jeans",2135,"Qqwerty12#")]
    [InlineData(1000,1000,"Jackets",123124.30,"12qwerty")]
    public void ShouldReturnValidValidation(long brandId, long subcategoryId, string name, double price, string desc)
    {
        ProductUpdateDto productUpdateDto = new ProductUpdateDto()
        {
            BrandId = brandId,
            SubCategoryId = subcategoryId,
            Name = name,
            UnitPrice = price,
            Description = desc
        };
        var validator = new ProductUpdateValidator();
        var result = validator.Validate(productUpdateDto);
        Assert.True(result.IsValid);
    }
}
