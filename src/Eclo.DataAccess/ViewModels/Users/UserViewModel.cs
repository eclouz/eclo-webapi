namespace Eclo.DataAccess.ViewModels.Users;

public class UserViewModel
{
    public long Id { get; set; }

    public string FirstName { get; set; } = String.Empty;

    public string LastName { get; set; } = String.Empty;

    public string PhoneNumber { get; set; } = String.Empty;

    public bool PhoneNumberConfirmed { get; set; }

    public string ImagePath { get; set; } = String.Empty;

    public string PassportSerialNumber { get; set; } = String.Empty;

    public DateTime BirthDate { get; set; }

    public string Region { get; set; } = String.Empty;

    public string District { get; set; } = String.Empty;

    public string Address { get; set; } = String.Empty;

}
