using Eclo.Application.Utilities;
using Eclo.DataAccess.Common;
using Eclo.Domain.Entities.Admins;
using static Dapper.SqlMapper;

namespace Eclo.DataAccess.Interfaces.Admins;

public interface IAdminRepository : IRepository<Admin, Admin>,
    IGetAll<Admin>, ISearchable<Admin>
{
    public Task<Admin?> GetByPhoneNumberAsync(string phoneNumber);

    public Task<IList<Admin>> GetAllAsync();
}
