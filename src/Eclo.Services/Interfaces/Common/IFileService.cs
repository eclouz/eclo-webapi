using Microsoft.AspNetCore.Http;

namespace Eclo.Services.Interfaces.Common;

public interface IFileService
{
    // returns subpath of image
    public Task<string> UploadImageAsync(IFormFile image);

    public Task<bool> DeleteImageAsync(string subpath);

    // returns subpath of avatar
    public Task<string> UploadAvatarAsync(IFormFile image);

    public Task<bool> DeleteAvatarAsync(string subpath);

    // returns subpath of icon
    public Task<string> UploadIconAsync(IFormFile image);

    public Task<bool> DeleteIconAsync(string subpath);
}