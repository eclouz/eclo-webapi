using System.ComponentModel.DataAnnotations;

namespace Eclo.Domain.Entities.Categories;

public class Category : Auditable
{
    [MaxLength(50)]
    public string Name { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;

}

