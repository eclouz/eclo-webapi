using Eclo.Persistence.Dtos.Brands;
using Eclo.Persistence.Validations.Brands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.Text;
using Xunit;

namespace Eclo.UnitTest.ValidatorTests.Brands;

public class BrandCreateValidatorTest
{
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
        BrandCreateDto brandCreateDto = new BrandCreateDto()
        {
            Name = "electronic products",
            BrandIconPath = imageFile
        };
        var validator = new BrandCreateValidator();
        var result = validator.Validate(brandCreateDto);
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
        BrandCreateDto brandCreateDto = new BrandCreateDto()
        {
            Name = "Electronic products",
            BrandIconPath = imageFile
        };
        var validator = new BrandCreateValidator();
        var result = validator.Validate(brandCreateDto);
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
        BrandCreateDto brandCreateDto = new BrandCreateDto()
        {
            Name = "Electronic products",
            BrandIconPath = imageFile
        };
        var validator = new BrandCreateValidator();
        var result = validator.Validate(brandCreateDto);
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
        BrandCreateDto brandCreateDto = new BrandCreateDto()
        {
            Name = "Electronic products",
            BrandIconPath = imageFile
        };
        var validator = new BrandCreateValidator();
        var result = validator.Validate(brandCreateDto);
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("Adidas")]
    [InlineData("Eagle")]
    [InlineData("Puma")]
    [InlineData("Nike")]
    [InlineData("Air Jordan")]
    [InlineData("Gucci")]
    [InlineData("H&M")]
    [InlineData("Versace")]
    [InlineData("Tommy Hilfiger")]
    [InlineData("Calvin Klein")]
    [InlineData("Forever 21")]
    public void ShouldReturnValidValidation(string name)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s");
        IFormFile imageFile = new FormFile(new MemoryStream(byteImage), 0, byteImage.Length, "data", "file.jpg");
        BrandCreateDto brandCreateDto = new BrandCreateDto()
        {
            Name = name,
            BrandIconPath = imageFile
        };
        var validator = new BrandCreateValidator();
        var result = validator.Validate(brandCreateDto);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("AA")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("A")]
    [InlineData("electronic products, we sell an electronic products to our clients, we sell an electronic products to our clients")]
    public void ShouldReturnInValidValidation(string name)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s");
        IFormFile imageFile = new FormFile(new MemoryStream(byteImage), 0, byteImage.Length, "data", "file.jpg");
        BrandCreateDto brandCreateDto = new BrandCreateDto()
        {
            Name = name,
            BrandIconPath = imageFile
        };
        var validator = new BrandCreateValidator();
        var result = validator.Validate(brandCreateDto);
        Assert.False(result.IsValid);
    }
}
