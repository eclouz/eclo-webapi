using Eclo.Persistence.Dtos.Auth;
using Eclo.Persistence.Validations.Auth;
using Xunit;

namespace Eclo.UnitTest.ValidatorTests.Auth;

public class RegisterValidatorTest
{
    [Theory]
    [InlineData("Abdulaziz", "Orazbaev", "+998331234567", "AAaa@@11")]
    [InlineData("Beka", "Erdashev", "+998335103546", "AAaa@@11")]
    [InlineData("Lilya", "Maratova", "+998335107898", "AAaa@@11")]
    [InlineData("Babamurat", "Karimbayev", "+998338101234", "AAaa@@11")]
    [InlineData("Dawran", "Sabirbayev", "+998935101256", "AAaa@@11")]
    [InlineData("Qahramon", "Anvarov", "+998335108545", "AAaa@@11")]
    [InlineData("Anatoliy", "Abdirov", "+998335106545", "AAaa@@11")]
    [InlineData("Najim", "Sattorov", "+998335103543", "AAaa@@11")]
    [InlineData("Lee", "Abdullaev", "+998335103547", "AAaa@@11")]
    [InlineData("Rustambek", "Qurbanbayev", "+998905103545", "AAaa@@11")]

    public void ShouldReturnValidRegisterValidation(string firstName, string lastName, string phoneNumber, string password)
    {
        var dto = new RegisterDto
        {
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            Password = password
        };

        var validationRules = new RegisterValidator();
        var result = validationRules.Validate(dto);

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("Jahongir", "Abdullaev", "+998335103545545", "AAaa@@11")]
    [InlineData("Beka", "Adhamov", "+99833510347474546", "@@#@$%$^")]
    [InlineData("", "Qodirov", "+998335107545", "1111111")]
    [InlineData("Fazliddin", "", "+998338103545", "AAAAAAAAA")]
    [InlineData("Akram", "Sodiqov", "+998335103544", "      ")]
    [InlineData("Qahramon", "Anvarov", "-998335108545", "AA")]
    [InlineData("T", "Adhamov", "+9978335106545", "AAAA11")]
    [InlineData("Nozim", "s", "+998335103543", "hhaa@@11")]
    [InlineData("Lee", "Abdullaev", "+99845335103547", "AAaa@@ii")]
    [InlineData("Jahongir", "Qubonaliyev", "+998905103545", "AAaaaa11")]

    public void ShouldReturnInvalidRegisterValidation(string firstName, string lastName, string phoneNumber, string password)
    {
        var dto = new RegisterDto
        {
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            Password = password
        };

        var validationRules = new RegisterValidator();
        var result = validationRules.Validate(dto);

        Assert.False(result.IsValid);
    }
}
