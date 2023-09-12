using Eclo.Persistence.Dtos.Auth;
using Eclo.Persistence.Validations.Auth;
using Xunit;

namespace Eclo.UnitTest.ValidatorTests.Auth;

public class HeadLoginValidatorTest
{
    [Theory]
    [InlineData("+998951092161", "asAS@#%123")]
    [InlineData("+998971234567", "Abcd123!@#")]
    [InlineData("+998998877665", "P@$$w0rd123")]
    [InlineData("+998935555555", "Qwerty!123")]
    [InlineData("+998940404040", "Hello123!")]
    [InlineData("+998950505050", "MyP@ssw0rd")]
    [InlineData("+998960606060", "OnMed!23!")]
    [InlineData("+998970707070", "Testing@123")]
    [InlineData("+998980808080", "Welcom@123")]
    [InlineData("+998990909090", "AAaa@@11")]

    public void ShouldReturnCorrectvalidation(string phone, string password)
    {
        var dto = new LoginDto()
        {
            PhoneNumber = phone,
            Password = password
        };

        var userLoginDto = new LoginValidator();
        var result = userLoginDto.Validate(dto);

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("-998951092161", "invalidpassword")]
    [InlineData("123456789", "asAS@#%123")]
    [InlineData("+998951092161", "")]
    [InlineData("invalidphone", "asAS@#%123")]
    [InlineData("+998951092161", "      ")]
    [InlineData("+99851092161", "123456789")]
    [InlineData("1092161", "AJSJDJWJDK")]
    [InlineData("+", "noSpecialCharacters")]
    [InlineData("+99895jbdsdb1092161", "")]
    [InlineData("+99895145092161", "noDigits#")]

    public void ShouldReturnIncorrectValidation(string phone, string password)
    {
        var dto = new LoginDto()
        {
            PhoneNumber = phone,
            Password = password
        };

        var validator = new LoginValidator();
        var result = validator.Validate(dto);

        Assert.False(result.IsValid);
    }
}
