using Microsoft.AspNetCore.Http;

namespace Eclo.Persistence.DTOs.Heads;

public class HeadUpdateDto
{
    public string FirstName { get; set; } = String.Empty;

    public string LastName { get; set; } = String.Empty;

    public string PhoneNumber { get; set; } = String.Empty;

    public string Password { get; set; } = String.Empty;

    public IFormFile? ImagePath { get; set; }

    public string PassportSerialNumber { get; set; } = String.Empty;

    public DateTime BirthDate { get; set; }

    public string Region { get; set; } = String.Empty;

    public string District { get; set; } = String.Empty;

    public string Address { get; set; } = String.Empty;
}
