using Eclo.Persistence.Dtos.Categories;
using Eclo.Persistence.Validations.Categories;
using Xunit;

namespace Eclo.UnitTest.ValidatorTests.Categories;

public class SubCategoryUpdateValidatorTest
{
    [Theory]
    [InlineData("AA")]
    [InlineData("05")]
    [InlineData("50")]
    [InlineData("A")]
    [InlineData("electronic products, we sell an electronic products to our clients, we sell an electronic products to our clients")]
    public void ShouldReturnInValidValidation(string name)
    {
        SubCategoryUpdateDto subCategoryUpdateDto = new SubCategoryUpdateDto()
        {
            CategoryId = 12,
            Name = name,
        };
        var validator = new SubCategoryUpdateValidator();
        var result = validator.Validate(subCategoryUpdateDto);
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("Jeans")]
    [InlineData("Blazer")]
    [InlineData("Suits")]
    [InlineData("Sweatshirts")]
    [InlineData("Shirt")]
    [InlineData("Sweaters")]
    [InlineData("Swimwears")]
    [InlineData("Jackets")]
    [InlineData("Coats")]
    [InlineData("Pants")]
    public void ShouldReturnValidValidation(string name)
    {
        SubCategoryUpdateDto subCategoryUpdateDto = new SubCategoryUpdateDto()
        {
            CategoryId = 12,
            Name = name,
        };
        var validator = new SubCategoryUpdateValidator();
        var result = validator.Validate(subCategoryUpdateDto);
        Assert.True(result.IsValid);
    }
}
