﻿using Eclo.Application.Exceptions.Files;
using Eclo.Application.Exceptions.Users;
using Eclo.DataAccess.Interfaces.Heads;
using Eclo.Domain.Entities.Heads;
using Eclo.Persistence.Dtos.Heads;
using Eclo.Persistence.DTOs.Heads;
using Eclo.Persistence.Helpers;
using Eclo.Services.Interfaces.Auth;
using Eclo.Services.Interfaces.Common;
using Eclo.Services.Interfaces.Heads;
using Eclo.Services.Security;

namespace Eclo.Services.Services.Heads;

public class HeadService : IHeadService
{
    private readonly IHeadRepository _headRepository;
    private readonly IFileService _fileService;
    private readonly IIdentityService _identityService;

    public HeadService(IHeadRepository headRepository,
        IFileService fileService,
        IIdentityService identityService)
    {
        this._headRepository = headRepository;
        this._fileService = fileService;
        this._identityService = identityService;
    }

    public async Task<bool> CreateAsync(HeadCreateDto dto)
    {
        var head = new Head();
        head.FirstName = dto.FirstName;
        head.LastName = dto.LastName;
        head.PhoneNumber = dto.PhoneNumber;
        head.PhoneNumberConfirmed = true;
        var security = PasswordHasher.Hash(dto.Password);
        head.PasswordHash = security.Hash;
        head.Salt = security.Salt;
        string filename = await _fileService.UploadAvatarAsync(dto.ImagePath!);
        head.ImagePath = filename;
        head.PassportSerialNumber = dto.PassportSerialNumber;
        head.BirthDate = dto.BirthDate;
        head.Region = dto.Region;
        head.District = dto.District;
        head.Address = dto.Address;
        head.CreatedAt = head.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _headRepository.CreateAsync(head);

        return dbResult > 0;
    }

    public async Task<bool> UpdateAsync(long headId, string phone, HeadUpdateDto dto)
    {
        var head = await _headRepository.GetByPhoneAsync(phone);
        if (head is null) throw new UserNotFoundException();

        head.FirstName = dto.FirstName;
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

        var dbResult = await _headRepository.UpdateAsync(headId, head);

        return dbResult > 0;
    }
}
