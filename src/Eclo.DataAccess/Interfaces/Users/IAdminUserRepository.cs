using Eclo.DataAccess.Common;
using Eclo.DataAccess.ViewModels.Users;
using Eclo.Domain.Entities.Users;

namespace Eclo.DataAccess.Interfaces.Users;

public interface IAdminUserRepository : IRepository<User, AdminUserViewModel>,
    IGetAll<AdminUserViewModel>, ISearchable<AdminUserViewModel>
{
    public Task<User?> GetById(long id);
}
