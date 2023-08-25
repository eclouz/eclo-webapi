using Eclo.DataAccess.Common;
using Eclo.DataAccess.ViewModels.Users;
using Eclo.Domain.Entities.Users;
using static Dapper.SqlMapper;

namespace Eclo.DataAccess.Interfaces.Users;

public interface IUserRepository : IRepository<User, UserViewModel>,
    IGetAll<UserViewModel>, ISearchable<UserViewModel>
{
    public Task<User?> GetByPhoneAsync(string phone);
    public Task<User?> GetById(long id);
    public Task<int> UpdatePhoneNumberAsync(string phoneNumber, User dto);
}