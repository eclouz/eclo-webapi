using Eclo.Persistence.Validations;
using Xunit;

namespace Eclo.UnitTest.ValidatorTests;

public class PhoneNumberValidatorTest
{
    [Theory]
    [InlineData("+998331211514")]
    [InlineData("+998901221311")]
    [InlineData("+998911511314")]
    [InlineData("+998651211374")]
    [InlineData("+998941211378")]
    [InlineData("+998931217384")]
    [InlineData("+998951214514")]
    [InlineData("+998771211414")]
    [InlineData("+998881211314")]
    [InlineData("+998971211114")]
    [InlineData("+998981211214")]
    [InlineData("+998991211334")]
    public void ShouldReturnCorrect(string phone)
    {
        var result = PhoneNumberValidator.IsValid(phone);
        Assert.True(result);
    }

    [Theory]
    [InlineData("998976261119")]
    [InlineData("AABBCCDD")]
    [InlineData("+9989762606191")]
    [InlineData("+99897626T619")]
    [InlineData("-99897626T619")]
    [InlineData("&99897626T619")]
    [InlineData("+99897626619")]
    [InlineData("+9989 626619")]
    [InlineData("+998 97 626 06 19")]
    [InlineData("+99890 082 50 50")]
    [InlineData("+99890-082-50-50")]
    [InlineData("+9989O0825O5O")]
    public void ShouldReturnWrong(string phone)
    {
        var result = PhoneNumberValidator.IsValid(phone);
        Assert.False(result);
    }
}
