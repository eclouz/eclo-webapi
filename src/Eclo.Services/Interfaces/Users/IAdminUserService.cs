using Eclo.Application.Utilities;
using Eclo.DataAccess.ViewModels.Users;

namespace Eclo.Services.Interfaces.Users;

public interface IAdminUserService
{
    public Task<bool> DeleteAsync(long userId);

    public Task<long> CountAsync();

    public Task<IList<AdminUserViewModel>> GetAllAsync(PaginationParams @params);

    public Task<AdminUserViewModel> GetByIdAsync(long userId);

    public Task<IList<AdminUserViewModel>> SearchAsync(string search, PaginationParams @params);
}
