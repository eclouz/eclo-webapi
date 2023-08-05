using System.ComponentModel.DataAnnotations;

namespace Eclo.Domain.Entities;

public class Human : Auditable
{
    [MaxLength(50)]
    public string FirstName { get; set; } = String.Empty;

    [MaxLength(50)]
    public string LastName { get; set; } = String.Empty;

    public DateTime BirthDate { get; set; }

    public string ImagePath { get; set; } = String.Empty;

}
