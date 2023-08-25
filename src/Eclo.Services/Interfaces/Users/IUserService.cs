using Eclo.Application.Utilities;
using Eclo.DataAccess.ViewModels.Users;
using Eclo.Persistence.Dtos.Users;

namespace Eclo.Services.Interfaces.Users;

public interface IUserService
{
    public Task<UserViewModel> GetByIdAsync(long userId);

    public Task<UserViewModel> GetByPhoneAsync(string phoneNumber, PaginationParams @params);
  
    public Task<bool> UpdateAsync(long userId, string phone, UserUpdateDto dto);
}
