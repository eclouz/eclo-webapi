using Eclo.Persistence.Validations;
using Xunit;

namespace Eclo.UnitTest.ValidatorTests;

public class SizeValidatorTest
{
    [Theory]
    [InlineData("XXX")]
    [InlineData("M")]
    [InlineData("150")]
    [InlineData("XXXL")]
    [InlineData("X")]
    [InlineData("XXL")]
    [InlineData("39")]
    [InlineData("35")]
    [InlineData("30")]
    [InlineData("40")]
    [InlineData("120")]
    [InlineData("XL")]
    public void ShouldReturnCorrect(string size)
    {
        var result = SizeValidator.IsValid(size);
        Assert.True(result);
    }

    [Theory]
    [InlineData("01")]
    [InlineData("02")]
    [InlineData("03")]
    [InlineData("00X")]
    [InlineData("X0")]
    [InlineData("XXS0")]
    [InlineData("XX00X")]
    [InlineData("SSX")]
    [InlineData("XXsS")]
    [InlineData("+XL")]
    [InlineData("XSXX")]
    [InlineData("557")]
    public void ShouldReturnWrong(string size)
    {
        var result = PhoneNumberValidator.IsValid(size);
        Assert.False(result);
    }
}
