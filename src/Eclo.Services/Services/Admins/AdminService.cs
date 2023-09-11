using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Admins;
using Eclo.Domain.Entities.Admins;
using Eclo.Persistence.DTOs.Admins;
using Eclo.Persistence.Helpers;
using Eclo.Services.Interfaces.Admins;
using Eclo.Services.Interfaces.Auth;
using Eclo.Services.Interfaces.Common;
using Eclo.Services.Security;

namespace Eclo.Services.Services.Admins;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _adminRepository;
    private readonly IFileService _fileService;
    private readonly IPaginator _paginator;
    private readonly IIdentityService _identityService;

    public AdminService(IAdminRepository adminRepository,
        IFileService fileService,
        IPaginator paginator,
        IIdentityService identityService)
    {
        this._adminRepository = adminRepository;
        this._fileService = fileService;
        this._paginator = paginator;
        this._identityService = identityService;
    }

    public Task<long> CountAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CreateAsync(AdminCreateDto dto)
    {
        var admin = new Admin();
        admin.FirstName = dto.FirstName;
        admin.LastName = dto.LastName;
        admin.PhoneNumber = dto.PhoneNumber;
        admin.PhoneNumberConfirmed = true;
        var security = PasswordHasher.Hash(dto.Password);
        admin.PasswordHash = security.Hash;
        admin.Salt = security.Salt;
        
        if (dto.ImagePath is not null)
        {
            string newImagePath = await _fileService.UploadAvatarAsync(dto.ImagePath);
            admin.ImagePath = newImagePath;
        }

        admin.PassportSerialNumber = dto.PassportSerialNumber;
        admin.BirthDate = dto.BirthDate;
        admin.Region = dto.Region;
        admin.District = dto.District;
        admin.Address = dto.Address;
        admin.CreatedAt = admin.UpdatedAt = TimeHelper.GetDateTime();

        var result = await _adminRepository.CreateAsync(admin);
        return result > 0;
    }

    public Task<bool> DeleteAsync(long adminId)
    {
        throw new NotImplementedException();
    }

    public Task<IList<Admin>> GetAllAsync(PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public Task<IList<Admin>> SearchAsync(string search)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(long adminId, AdminUpdateDto dto)
    {
        throw new NotImplementedException();
    }
}
