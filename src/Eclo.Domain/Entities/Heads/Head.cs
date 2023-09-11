namespace Eclo.Domain.Entities.Heads;

public class Head : Human
{
    public string PhoneNumber { get; set; } = String.Empty;

    public bool PhoneNumberConfirmed { get; set; }

    public string PasswordHash { get; set; } = string.Empty;

    public string Salt { get; set; } = string.Empty;

    public string PassportSerialNumber { get; set; } = string.Empty;

    public string Region { get; set; } = String.Empty;

    public string District { get; set; } = String.Empty;

    public string Address { get; set; } = String.Empty;
}
