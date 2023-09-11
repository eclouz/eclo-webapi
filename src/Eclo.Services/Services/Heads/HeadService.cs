﻿using Eclo.Application.Exceptions.Files;
using Eclo.Application.Exceptions.Users;
using Eclo.DataAccess.Interfaces.Heads;
using Eclo.Persistence.DTOs.Heads;
using Eclo.Persistence.Helpers;
using Eclo.Services.Interfaces.Common;
using Eclo.Services.Interfaces.Heads;

namespace Eclo.Services.Services.Heads;

public class HeadService : IHeadService
{
    private readonly IHeadRepository _headRepository;
    private readonly IFileService _fileService;

    public HeadService(IHeadRepository headRepository,
        IFileService fileService)
    {
        this._headRepository = headRepository;
        this._fileService = fileService;
    }

    public async Task<bool> UpdateAsync(long headId, string phone, HeadUpdateDto dto)
    {
        var head = await _headRepository.GetByPhoneAsync(phone);
        if (head is null) throw new UserNotFoundException();

        head.FirstName = dto.FirstName;
        head.LastName = dto.LastName;

        if (dto.ImagePath is not null)
        {
            if (head.ImagePath != "avatars\\avatar.png")
            {
                var deleteResult = await _fileService.DeleteAvatarAsync(head.ImagePath);
                if (deleteResult is false) throw new ImageNotFoundException();
            }

            string newImagePath = await _fileService.UploadAvatarAsync(dto.ImagePath);

            head.ImagePath = newImagePath;
        }

        var checkPhone = await _headRepository.GetByPhoneAsync(dto.PhoneNumber);
        if (checkPhone is null || phone == dto.PhoneNumber)
        {
            head.PhoneNumber = dto.PhoneNumber;
        }
        else throw new UserAlreadyExistsException(dto.PhoneNumber);

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