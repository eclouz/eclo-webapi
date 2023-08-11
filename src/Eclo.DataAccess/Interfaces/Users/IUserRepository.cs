using Eclo.DataAccess.Common;
using Eclo.DataAccess.ViewModels.Users;
using Eclo.Domain.Entities.Users;

namespace Eclo.DataAccess.Interfaces.Users;

public interface IUserRepository : IRepository<User, UserViewModel>,
    IGetAll<UserViewModel>, ISearchable<UserViewModel>
{
    public Task<User?> GetByPhoneAsync(string phone);
}