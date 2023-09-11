using Eclo.Persistence.Dtos.Categories;
using Eclo.Persistence.Validations.Categories;
using Xunit;

namespace Eclo.UnitTest.ValidatorTests.Categories;

public class CategoryCreateValidatorTest
{
    [Theory]
    [InlineData("AA", "we")]
    [InlineData("001", " ")]
    [InlineData("05", "")]
    [InlineData("50", "WW")]
    [InlineData("QE45dsfaAd45","#A")]
    [InlineData("A","We")]
    [InlineData("A514-A","tt")]
    [InlineData("/_fcdAA", "%w")]
    [InlineData("AA_=","#2")]
    [InlineData("electronic products, we sell an electronic products to our clients, we sell an electronic products to our clients", "22")]
    public void ShouldReturnInValidValidation(string name, string description)
    {
        CategoryCreateDto categoryCreateDto = new CategoryCreateDto()
        {
            Name = name,
            Description = description
        };
        var validator = new CategoryCreateValidator();
        var result = validator.Validate(categoryCreateDto);
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("Jeans", "Desc")]
    [InlineData("Cap", "Des")]
    public void ShouldReturnValidValidation(string name, string description)
    {
        CategoryCreateDto categoryCreateDto = new CategoryCreateDto()
        {
            Name = name,
            Description = description
        };
        var validator = new CategoryCreateValidator();
        var result = validator.Validate(categoryCreateDto);
        Assert.True(result.IsValid);
    }
}
