using Eclo.Persistence.Helpers;
using Eclo.Services.Interfaces.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Eclo.Services.Services.Common;

public class FileService : IFileService
{
    private readonly string STORAGE = "storage";
    private readonly string IMAGES = "images";
    private readonly string ICONS = "icons";
    private readonly string PRODUCTS = "products";
    private readonly string BRANDS = "brands";
    private readonly string AVATARS = "avatars";
    private readonly string ROOTPATH;

    public FileService(IWebHostEnvironment env)
    {
        ROOTPATH = env.WebRootPath;
    }

    public async Task<bool> DeleteAvatarAsync(string subpath)
    {
        string path = Path.Combine(ROOTPATH, subpath);
        if (File.Exists(path))
        {
            await Task.Run(() =>
            {
                File.Delete(path);
            });
            return true;
        }
        else return false;
    }

    public async Task<bool> DeleteIconAsync(string subpath)
    {
        string path = Path.Combine(ROOTPATH, subpath);
        if (File.Exists(path))
        {
            await Task.Run(() =>
            {
                File.Delete(path);
            });
            return true;
        }
        else return false;
    }

    public async Task<bool> DeleteImageAsync(string subpath)
    {
        string path = Path.Combine(ROOTPATH, subpath);
        if (File.Exists(path))
        {
            await Task.Run(() =>
            {
                File.Delete(path);
            });
            return true;
        }
        else return false;
    }

    public async Task<string> UploadAvatarAsync(IFormFile image)
    {
        string newImageName = MediaHelper.MakeImageName(image.FileName);
        string subpath = Path.Combine(AVATARS, newImageName);
        string path = Path.Combine(ROOTPATH, subpath);

        var stream = new FileStream(path, FileMode.Create);
        await image.CopyToAsync(stream);
        stream.Close();

        return subpath;
    }

    public async Task<string> UploadIconAsync(IFormFile image)
    {
        string newImageName = MediaHelper.MakeImageName(image.FileName);
        string subpath = Path.Combine(STORAGE, ICONS, BRANDS, newImageName);
        string path = Path.Combine(ROOTPATH, subpath);

        var stream = new FileStream(path, FileMode.Create);
        await image.CopyToAsync(stream);
        stream.Close();

        return subpath;
    }

    public async Task<string> UploadImageAsync(IFormFile image)
    {
        string newImageName = MediaHelper.MakeImageName(image.FileName);
        string subpath = Path.Combine(STORAGE, IMAGES, PRODUCTS, newImageName);
        string path = Path.Combine(ROOTPATH, subpath);

        var stream = new FileStream(path, FileMode.Create);
        await image.CopyToAsync(stream);
        stream.Close();

        return subpath;
    }
}
