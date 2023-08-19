﻿using Microsoft.AspNetCore.Http;

namespace Eclo.Persistence.Dtos.Users;

public class UserCreateDto
{
    public IFormFile ImagePath { get; set; } = default!;

    public string PhoneNumber { get; set; } = String.Empty;

    public bool PhoneNumberConfirmed { get; set; }

    public string PasswordHash { get; set; } = String.Empty;

    public string Salt { get; set; } = String.Empty;

    public string PassportSerialNumber { get; set; } = String.Empty;

    public string Region { get; set; } = String.Empty;

    public string District { get; set; } = String.Empty;

    public string Address { get; set; } = String.Empty;
}
