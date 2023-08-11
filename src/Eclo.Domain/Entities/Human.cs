using System.ComponentModel.DataAnnotations;

namespace Eclo.Domain.Entities;

public class Human : Auditable
{
    public string FirstName { get; set; } = String.Empty;

    public string LastName { get; set; } = String.Empty;

    public DateTime BirthDate { get; set; }

    public string ImagePath { get; set; } = String.Empty;

}
