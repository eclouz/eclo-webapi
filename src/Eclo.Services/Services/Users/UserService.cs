using Eclo.Application.Exceptions.Files;
using Eclo.Application.Exceptions.Users;
using Eclo.DataAccess.Interfaces.Users;
using Eclo.DataAccess.ViewModels.Users;
using Eclo.Persistence.Dtos.Users;
using Eclo.Persistence.Helpers;
using Eclo.Services.Interfaces.Common;
using Eclo.Services.Interfaces.Users;

namespace Eclo.Services.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IFileService _fileService;

    public UserService(IUserRepository userRepository,
        IFileService fileService)
    {
        this._repository = userRepository;
        this._fileService = fileService;
    }

    public async Task<UserViewModel> GetByIdAsync(long userId)
    {
        var user = await _repository.GetByIdAsync(userId);
        if (user == null) throw new UserNotFoundException();
        else return user;
    }

    public async Task<bool> UpdateAsync(long userId, UserUpdateDto dto)
    {
        var user = await _repository.GetById(userId);
        if (user == null) throw new UserNotFoundException();

        // update user with new items 
        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;

        if (dto.ImagePath is not null)
        {
            // delete old avatar
            var deleteResult = await _fileService.DeleteAvatarAsync(user.ImagePath);
            if (deleteResult is false) throw new ImageNotFoundException();

            // upload new avatar
            string newImagePath = await _fileService.UploadAvatarAsync(dto.ImagePath);

            // parse new path to avatar
            user.ImagePath = newImagePath;
        }
        // else user old avatar is have to save

        user.PassportSerialNumber = dto.PassportSerialNumber;
        user.BirthDate = dto.BirthDate;
        user.Region = dto.Region;
        user.District = dto.District;
        user.Address = dto.Address;
        user.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _repository.UpdateAsync(userId, user);

        return dbResult > 0;
    }
}
