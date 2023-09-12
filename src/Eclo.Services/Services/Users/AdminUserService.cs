using Eclo.Application.Exceptions.Files;
using Eclo.Application.Exceptions.Users;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Users;
using Eclo.DataAccess.ViewModels.Users;
using Eclo.Services.Interfaces.Common;
using Eclo.Services.Interfaces.Users;

namespace Eclo.Services.Services.Users;

public class AdminUserService : IAdminUserService
{
    private readonly IAdminUserRepository _repository;
    private readonly IFileService _fileService;
    private readonly IPaginator _paginator;

    public AdminUserService(IAdminUserRepository adminUserRepository,
        IFileService fileService,
        IPaginator paginator)
    {
        this._repository = adminUserRepository;
        this._fileService = fileService;
        this._paginator = paginator;
    }

    public async Task<long> CountAsync() => await _repository.CountAsync();

    public async Task<bool> DeleteAsync(long userId)
    {
        var user = await _repository.GetById(userId);
        if (user == null) throw new UserNotFoundException();

        if (user.ImagePath != "avatars\\avatar.png")
        {
            var result = await _fileService.DeleteAvatarAsync(user.ImagePath);
            if (result == false) throw new ImageNotFoundException();
        }

        var dbResult = await _repository.DeleteAsync(userId);

        return dbResult > 0;
    }

    public async Task<IList<AdminUserViewModel>> GetAllAsync(PaginationParams @params)
    {
        var users = await _repository.GetAllAsync(@params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);

        return users;
    }

    public async Task<AdminUserViewModel> GetByIdAsync(long userId)
    {
        var user = await _repository.GetByIdAsync(userId);
        if (user == null) throw new UserNotFoundException();
        else return user;
    }

    public async Task<IList<AdminUserViewModel>> SearchAsync(string search, PaginationParams @params)
    {
        var users = await _repository.SearchAsync(search, @params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);

        return users.Item2.ToList();
    }
}
