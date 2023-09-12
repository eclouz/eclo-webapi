using Eclo.Persistence.Dtos.Auth;
using Eclo.Persistence.Validations.Auth;
using Xunit;

namespace Eclo.UnitTest.ValidatorTests.Auth;

public class VerifyRegisterValidatorTest
{
    [Theory]
    [InlineData("+998951092161", 11111)]
    [InlineData("+998971234567", 11111)]
    [InlineData("+998998877665", 11111)]
    [InlineData("+998935555555", 11111)]
    [InlineData("+998940404040", 11111)]
    [InlineData("+998950505050", 11111)]
    [InlineData("+998960606060", 11111)]
    [InlineData("+998970707070", 11111)]
    [InlineData("+998980808080", 11111)]
    [InlineData("+998990909090", 11111)]
    public void ShouldReturnCorrectvalidation(string phone, int code)
    {
        var dto = new VerifyRegisterDto()
        {
            PhoneNumber = phone,
            Code = code
        };

        var userLoginDto = new VerifyRegisterValidator();
        var result = userLoginDto.Validate(dto);

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("-998951092161", 1)]
    [InlineData("123456789", 122222)]
    [InlineData("+998951092161", 000)]
    [InlineData("invalidphone", 0)]
    [InlineData("+998951092161", 10000000)]
    [InlineData("+99851092161", 1234)]
    [InlineData("1092161", 999999)]
    [InlineData("+", 54)]
    [InlineData("+99895jbdsdb1092161", 900)]
    [InlineData("+99895145092161", 123333)]
    public void ShouldReturnInCorrectvalidation(string phone, int code)
    {
        var dto = new VerifyRegisterDto()
        {
            PhoneNumber = phone,
            Code = code
        };

        var userLoginDto = new VerifyRegisterValidator();
        var result = userLoginDto.Validate(dto);

        Assert.False(result.IsValid);
    }
}
