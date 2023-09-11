﻿using Eclo.Persistence.DTOs.Heads;

namespace Eclo.Services.Interfaces.Heads;

public interface IHeadService
{
    public Task<bool> UpdateAsync(long userId, string phone, HeadUpdateDto dto);
}