using Eclo.Application.Exceptions.Admins;
using Eclo.Application.Exceptions.Files;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Admins;
using Eclo.DataAccess.Interfaces.Heads;
using Eclo.Domain.Entities;
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
    private readonly IIdentityService _identity;
    private readonly IHeadRepository _headRepository;

    public AdminService(IAdminRepository adminRepository,
        IFileService fileService,
        IPaginator paginator,
        IIdentityService identityService,
        IHeadRepository headRepository)
    {
        this._adminRepository = adminRepository;
        this._fileService = fileService;
        this._paginator = paginator;
        this._identity = identityService;
        this._headRepository = headRepository;
    }

    public async Task<long> CountAsync() => await _adminRepository.CountAsync();

    public async Task<bool> CreateAsync(AdminCreateDto dto)
    {
        var admins = await _adminRepository.GetAllAsync();
        bool check = false;
        for (var i = 0; i < admins.Count; i++)
        {
            if (admins[i].PhoneNumber == dto.PhoneNumber) throw new AdminAlreadyExistsException();
            else check = true;
        }
        if (check)
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
        else throw new AdminAlreadyExistsException();
    }

    public async Task<bool> DeleteAsync(long adminId)
    {
        var admin = await _adminRepository.GetByIdAsync(adminId);
        if (admin == null) throw new AdminNotFoundException();

        if (admin.ImagePath != null)
        {
            var result = await _fileService.DeleteAvatarAsync(admin.ImagePath);
            if (result == false) throw new ImageNotFoundException();
        }

        var dbResult = await _adminRepository.DeleteAsync(adminId);

        return dbResult > 0;
    }

    public async Task<IList<Admin>> GetAllAsync(PaginationParams @params)
    {
        var admins = await _adminRepository.GetAllAsync(@params);
        var count = await _adminRepository.CountAsync();
        _paginator.Paginate(count, @params);

        return admins;
    }

    public async Task<Human> GetByIdAsync()
    {
        if (_identity.IdentityRole.ToString() == "Head")
        {
            var head = await _headRepository.GetByIdAsync(_identity.Id);
            return head!;
        }
        else
        {
            var admin = await _adminRepository.GetByIdAsync(_identity.Id);
            if (admin == null) throw new AdminNotFoundException();
            else return admin;
        }
    }

    public async Task<IList<Admin>> SearchAsync(string search, PaginationParams @params)
    {
        var admins = await _adminRepository.SearchAsync(search, @params);
        var count = await _adminRepository.CountAsync();
        _paginator.Paginate(count, @params);

        return admins.Item2.ToList();
    }

    public async Task<bool> UpdateAdminAsync(long adminId, AdminUpdateDto dto)
    {
        var admin = await _adminRepository.GetByIdAsync(adminId);
        if (admin == null) throw new AdminNotFoundException();

        admin.FirstName = dto.FirstName;
        admin.LastName = dto.LastName;
        var security = PasswordHasher.Hash(dto.Password);
        admin.PasswordHash = security.Hash;
        admin.Salt = security.Salt;

        if (dto.ImagePath is not null)
        {
            var deleteResult = await _fileService.DeleteAvatarAsync(admin.ImagePath);
            if (deleteResult is false) throw new ImageNotFoundException();

            string newImagePath = await _fileService.UploadAvatarAsync(dto.ImagePath);
            admin.ImagePath = newImagePath;
        }
        admin.PhoneNumber = dto.PhoneNumber;
        admin.PhoneNumberConfirmed = true;
        admin.PassportSerialNumber = dto.PassportSerialNumber;
        admin.BirthDate = dto.BirthDate;
        admin.Region = dto.Region;
        admin.District = dto.District;
        admin.Address = dto.Address;
        admin.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _adminRepository.UpdateAsync(adminId, admin);

        return dbResult > 0;
    }

    public async Task<bool> UpdateAsync(long adminId, AdminUpdateDto dto)
    {
        if (_identity.IdentityRole.ToString() == "Head")
        {
            var head = await _headRepository.GetByIdAsync(_identity.Id);

            head!.FirstName = dto.FirstName;
            head.LastName = dto.LastName;
            var security = PasswordHasher.Hash(dto.Password);
            head.PasswordHash = security.Hash;
            head.Salt = security.Salt;

            if (dto.ImagePath is not null)
            {
                var deleteResult = await _fileService.DeleteAvatarAsync(head.ImagePath);
                if (deleteResult is false) throw new ImageNotFoundException();

                string newImagePath = await _fileService.UploadAvatarAsync(dto.ImagePath);
                head.ImagePath = newImagePath;
            }

            head.PhoneNumber = dto.PhoneNumber;
            head.PhoneNumberConfirmed = true;
            head.PassportSerialNumber = dto.PassportSerialNumber;
            head.BirthDate = dto.BirthDate;
            head.Region = dto.Region;
            head.District = dto.District;
            head.Address = dto.Address;
            head.UpdatedAt = TimeHelper.GetDateTime();

            var dbResult = await _headRepository.UpdateAsync(_identity.Id, head);

            return dbResult > 0;
        }
        else
        {
            var admin = await _adminRepository.GetByIdAsync(adminId);
            if (admin == null) throw new AdminNotFoundException();

            admin.FirstName = dto.FirstName;
            admin.LastName = dto.LastName;
            var security = PasswordHasher.Hash(dto.Password);
            admin.PasswordHash = security.Hash;
            admin.Salt = security.Salt;

            if (dto.ImagePath is not null)
            {
                var deleteResult = await _fileService.DeleteAvatarAsync(admin.ImagePath);
                if (deleteResult is false) throw new ImageNotFoundException();

                string newImagePath = await _fileService.UploadAvatarAsync(dto.ImagePath);
                admin.ImagePath = newImagePath;
            }
            admin.PhoneNumber = dto.PhoneNumber;
            admin.PhoneNumberConfirmed = true;
            admin.PassportSerialNumber = dto.PassportSerialNumber;
            admin.BirthDate = dto.BirthDate;
            admin.Region = dto.Region;
            admin.District = dto.District;
            admin.Address = dto.Address;
            admin.UpdatedAt = TimeHelper.GetDateTime();

            var dbResult = await _adminRepository.UpdateAsync(adminId, admin);

            return dbResult > 0;
        }
    }
}
