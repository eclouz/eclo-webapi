using Eclo.Application.Exceptions.Files;
using Eclo.Application.Exceptions.Users;
using Eclo.DataAccess.Interfaces.Users;
using Eclo.DataAccess.Repositories.Users;
using Eclo.DataAccess.ViewModels.Users;
using Eclo.Persistence.Dtos.Users;
using Eclo.Persistence.Helpers;
using Eclo.Services.Interfaces.Auth;
using Eclo.Services.Interfaces.Common;
using Eclo.Services.Interfaces.Users;

namespace Eclo.Services.Services.Users;

public class UserService : IUserService
{
    private readonly IIdentityService _identity;
    private readonly IUserRepository _repository;
    private readonly IFileService _fileService;

    public UserService(IUserRepository userRepository,
        IFileService fileService,
        IIdentityService identity)
    {
        this._repository = userRepository;
        this._fileService = fileService;
        this._identity = identity;
    }

    public async Task<UserViewModel> GetByIdAsync(long userId)
    {
        var user = await _repository.GetByIdAsync(userId);
        if (user == null) throw new UserNotFoundException();
        else return user;
    }

    public async Task<bool> UpdateAsync(long userId, string phone, UserUpdateDto dto)
    {
        var user = await _repository.GetByPhoneAsync(phone);
        if (user is null) throw new UserNotFoundException();

        // update user with new items 
        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;

        if (dto.ImagePath is not null)
        {
            // delete old avatar
            if (user.ImagePath != "avatars\\avatar.png") 
            {
                var deleteResult = await _fileService.DeleteAvatarAsync(user.ImagePath);
                if (deleteResult is false) throw new ImageNotFoundException();
            }

            // upload new avatar
            string newImagePath = await _fileService.UploadAvatarAsync(dto.ImagePath);

            // parse new path to avatar
            user.ImagePath = newImagePath;
        }
        // else user old avatar is have to save
        
        var checkPhone = await _repository.GetByPhoneAsync(dto.PhoneNumber);
        if(checkPhone is null || phone == dto.PhoneNumber)
        {
            user.PhoneNumber = dto.PhoneNumber;
        }
        else throw new UserAlreadyExistsException(dto.PhoneNumber);

        user.PassportSerialNumber = dto.PassportSerialNumber;
        user.BirthDate = dto.BirthDate;
        user.Region = dto.Region;
        user.District = dto.District;
        user.Address = dto.Address;
        user.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _repository.UpdateAsync(userId , user);

        return dbResult > 0;
    }
}
