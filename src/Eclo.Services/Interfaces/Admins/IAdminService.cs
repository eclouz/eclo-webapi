using Eclo.Application.Utilities;
using Eclo.Domain.Entities.Admins;
using Eclo.Persistence.DTOs.Admins;

namespace Eclo.Services.Interfaces.Admins;

public interface IAdminService
{
    public Task<IList<Admin>> GetAllAsync(PaginationParams @params);

    public Task<bool> CreateAsync(AdminCreateDto dto);

    public Task<bool> UpdateAsync(long adminId, AdminUpdateDto dto);

    public Task<bool> DeleteAsync(long adminId);

    public Task<long> CountAsync();

    public Task<IList<Admin>> SearchAsync(string search, PaginationParams @params);
}
