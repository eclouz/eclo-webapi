using Eclo.Persistence.Dtos.Categories;
using Eclo.Persistence.Validations.Categories;
using Xunit;

namespace Eclo.UnitTest.ValidatorTests.Categories;

public class SubCategoryCreateValidatorTest
{
    [Theory]
    [InlineData("AA")]
    [InlineData("05")]
    [InlineData("50")]
    [InlineData("A")]
    [InlineData("electronic products, we sell an electronic products to our clients, we sell an electronic products to our clients")]
    public void ShouldReturnInValidValidation(string name)
    {
        SubCategoryCreateDto subCategoryCreateDto = new SubCategoryCreateDto()
        {
            Name = name
        };
        var validator = new SubCategoryCreateValidator();
        var result = validator.Validate(subCategoryCreateDto);
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("Cap")]
    [InlineData("Jeans")]
    public void ShouldReturnValidValidation(string name)
    {
        SubCategoryCreateDto subCategoryCreateDto = new SubCategoryCreateDto()
        {
            Name = name,
        };
        var validator = new SubCategoryCreateValidator();
        var result = validator.Validate(subCategoryCreateDto);
        Assert.True(result.IsValid);
    }
}
