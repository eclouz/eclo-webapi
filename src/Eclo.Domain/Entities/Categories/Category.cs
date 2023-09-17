namespace Eclo.Domain.Entities.Categories;

public class Category : Auditable
{
    public string Name { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;

}

