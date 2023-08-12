using Eclo.Persistence.Dtos.Categories;
using Eclo.Persistence.Validations.Categories;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;
using System.Text;
using Xunit;

namespace Eclo.UnitTest.ValidatorTests.Categories;

public class CategoryCreateValidatorTest
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
        CategoryCreateDto categoryCreateDto = new CategoryCreateDto()
        {
            Name = name,
        };
        var validator = new CategoryCreateValidator();
        var result = validator.Validate(categoryCreateDto);
        Assert.False(result.IsValid);
    }
    [Theory]
    [InlineData("AliSher")]
    [InlineData("Jasur")]
    [InlineData("Komil")]
    [InlineData("Sardor")]
    [InlineData("Muhammadamin")]
    [InlineData("Ali")]
    [InlineData("Jabbor")]
    [InlineData("Azam")]
    [InlineData("Umarali")]
    [InlineData("Bobur")]
    [InlineData("Abbos")]
    public void ShouldReturnValidValidation(string name)
    {
        CategoryCreateDto categoryCreateDto = new CategoryCreateDto()
        {
            Name = name,
        };
        var validator = new CategoryCreateValidator();
        var result = validator.Validate(categoryCreateDto);
        Assert.False(result.IsValid);
    }
}
