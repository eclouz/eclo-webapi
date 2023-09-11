using Microsoft.AspNetCore.Http;

namespace Eclo.Persistence.DTOs.Admins;

public class AdminUpdateDto
{
    public string FirstName { get; set; } = String.Empty;

    public string LastName { get; set; } = String.Empty;

    public IFormFile? ImagePath { get; set; } = default!;

    public DateTime BirthDate { get; set; }

    public string PhoneNumber { get; set; } = String.Empty;

    public string Password { get; set; } = String.Empty;

    public string PassportSerialNumber { get; set; } = String.Empty;

    public string Region { get; set; } = String.Empty;

    public string District { get; set; } = String.Empty;

    public string Address { get; set; } = String.Empty;
}
