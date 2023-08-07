using Eclo.DataAccess.Common;
using Eclo.Domain.Entities.Admins;

namespace Eclo.DataAccess.Interfaces.Admins;

public interface IAdminRepository : IRepository<Admin, Admin>,
    IGetAll<Admin>, ISearchable<Admin>
{
}
