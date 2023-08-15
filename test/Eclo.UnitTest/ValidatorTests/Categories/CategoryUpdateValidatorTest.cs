﻿using Eclo.Persistence.Dtos.Categories;
using Eclo.Persistence.Validations.Categories;
using FluentValidation;
using Xunit;

namespace Eclo.UnitTest.ValidatorTests.Categories;

public class CategoryUpdateValidatorTest
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
        CategoryUpdateDto categoryUpdateDto = new CategoryUpdateDto()
        {
            Name = name,
        };
        var validator = new CategoryUpdateValidator();
        var result = validator.Validate(categoryUpdateDto);
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
        CategoryUpdateDto categoryUpdateDto = new CategoryUpdateDto()
        {
            Name = name,
        };
        var validator = new CategoryUpdateValidator();
        var result = validator.Validate(categoryUpdateDto);
        Assert.True(result.IsValid);
    }
}
