using Eclo.Persistence.DTOs.Admins;
using Eclo.Persistence.Validations.Admins;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.Text;
using Xunit;

namespace Eclo.UnitTest.ValidatorTests.Admins;

public class AdminCreateValidatorTest
{
    [Theory]
    [InlineData("", "orazbaev", "aa0561687", "+998335103545545", "@@#@$%$^")]
    [InlineData("A", "Orazbaev989", " KA0561687", "+998335107545", "1111111")]
    [InlineData("#", "123124234", "KA05616", "+998338103545", "AAAAAAAAA")]
    [InlineData("13234242", "#", "KA0561687978", "+998335103544", "     ")]
    [InlineData("", "", " 0561687", "-998335108545", "AA")]
    [InlineData(" ", " ", "", "+9978335106545", "AAAA11")]
    [InlineData("abdulazizbekxantemirjuniorasfsf", "asfsdhgthtrhwrthrhrhrhrhwr", "000987896237", "+998335103543", "hhaa@@11")]
    [InlineData("Abdulazizbekxantemirjuniorasfsf", "Asfsdhgthtrhwrthrhrhrhrhwr", "000987896237", "+99845335103547", "AAaa@@ii")]
    [InlineData("AIERTURWHLGRJTHBDRJGHKDTRHJR", "SHDFGEHGRHJMEGFWMRHGRGRTHETHTYHTE", "000987896237", "+998905103545", "AAaaaa11")]
    public void ShouldReturnInValidValidationResult(string firstname, string lastname, string passportSerialNumber, string phone, string password)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s");
        IFormFile imageFile = new FormFile(new MemoryStream(byteImage), 0, byteImage.Length, "data", "file.jpg");
        var adminCreateDto = new AdminCreateDto()
        {
            FirstName = firstname,
            LastName = lastname,
            PassportSerialNumber = passportSerialNumber,
            PhoneNumber = phone,
            Password = password,
            ImagePath = imageFile
        };

        var admin = new AdminCreateValidator();
        var result = admin.Validate(adminCreateDto);
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("Abdulaziz", "Orazbaev", "KA0561687", "+998933644016", "#CSharp2023")]
    [InlineData("Gulnur", "Boranbaeva", "CK0561687", "+998903644016", "#CSharp2023")]
    [InlineData("Sherzod", "Yuldashev", "AD0561687", "+998883644016", "#CSharp2023")]
    [InlineData("Adriano", "Guliermo", "AD0561687", "+998713644016", "#CSharp2023")]
    [InlineData("Beknazar", "Rasulov", "KK0561687", "+998913644016", "#CSharp2023")]
    [InlineData("Aza", "Kim", "AA0561687", "+998943644016", "#CSharp2023")]
    public void ShouldReturnValidValidationResult(string firstname, string lastname, string passportSerialNumber, string phone, string password)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s");
        IFormFile imageFile = new FormFile(new MemoryStream(byteImage), 0, byteImage.Length, "data", "file.jpg");
        var adminCreateDto = new AdminCreateDto()
        {
            FirstName = firstname,
            LastName = lastname,
            PassportSerialNumber = passportSerialNumber,
            PhoneNumber = phone,
            Password = password,
            ImagePath = imageFile
        };

        var admin = new AdminCreateValidator();
        var result = admin.Validate(adminCreateDto);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(9.5)]
    [InlineData(5.1)]
    public void ShouldReturnWrongImageFileSize(double imageSizeMB)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("we sell an electronic products to our clients");
        long imageSizeInBytes = (long)(imageSizeMB * 1024 * 1024);
        IFormFile imageFile = new FormFile(new MemoryStream(byteImage), 0, imageSizeInBytes, "data", "file.png");
        AdminCreateDto admin = new AdminCreateDto()
        {
            FirstName = "Abdulaziz",
            LastName = "Orazbaev",
            PassportSerialNumber = "KA0561687",
            PhoneNumber = "+998933644016",
            Password = "#CSharp2023",
            ImagePath = imageFile
        };
        var validator = new AdminCreateValidator();
        var result = validator.Validate(admin);
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData(2.95)]
    [InlineData(3)]
    [InlineData(2.5)]
    [InlineData(1)]
    [InlineData(0.5)]
    [InlineData(0.75)]
    public void ShouldReturnValidImageFileSize(double imageSizeMB)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("we sell an electronic products to our clients");
        long imageSizeInBytes = (long)(imageSizeMB * 1024 * 1024);
        IFormFile imageFile = new FormFile(new MemoryStream(byteImage), 0, imageSizeInBytes, "data", "file.png");
        AdminCreateDto admin = new AdminCreateDto()
        {
            FirstName = "Abdulaziz",
            LastName = "Orazbaev",
            PassportSerialNumber = "KA0561687",
            PhoneNumber = "+998933644016",
            Password = "#CSharp2023",
            ImagePath = imageFile
        };
        var validator = new AdminCreateValidator();
        var result = validator.Validate(admin);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("file.png")]
    [InlineData("file.jpg")]
    [InlineData("file.jpeg")]
    [InlineData("file.bmp")]
    [InlineData("file.svg")]
    public void ShouldReturnCorrectImageFileExtension(string imagename)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s");
        IFormFile imageFile = new FormFile(new MemoryStream(byteImage), 0, byteImage.Length, "data", imagename);
        AdminCreateDto admin = new AdminCreateDto()
        {
            FirstName = "Abdulaziz",
            LastName = "Orazbaev",
            PassportSerialNumber = "KA0561687",
            PhoneNumber = "+998933644016",
            Password = "#CSharp2023",
            ImagePath = imageFile
        };
        var validator = new AdminCreateValidator();
        var result = validator.Validate(admin);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("file.zip")]
    [InlineData("file.mp3")]
    [InlineData("file.html")]
    [InlineData("file.gif")]
    [InlineData("file.txt")]
    [InlineData("file.HEIC")]
    [InlineData("file.mp4")]
    [InlineData("file.avi")]
    [InlineData("file.mvk")]
    [InlineData("file.vaw")]
    [InlineData("file.webp")]
    [InlineData("file.pdf")]
    [InlineData("file.doc")]
    [InlineData("file.docx")]
    [InlineData("file.xls")]
    [InlineData("file.exe")]
    [InlineData("file.dll")]
    public void ShouldReturnWrongImageFileExtension(string imagename)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s");
        IFormFile imageFile = new FormFile(new MemoryStream(byteImage), 0, byteImage.Length, "data", imagename);
        AdminCreateDto admin = new AdminCreateDto()
        {
            FirstName = "Abdulaziz",
            LastName = "Orazbaev",
            PassportSerialNumber = "KA0561687",
            PhoneNumber = "+998933644016",
            Password = "#CSharp2023",
            ImagePath = imageFile
        };
        var validator = new AdminCreateValidator();
        var result = validator.Validate(admin);
        Assert.False(result.IsValid);
    }
}
