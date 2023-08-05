namespace Eclo.Domain.Entities.Categories;

public class SubCategory : Auditable
{
    public long CategoryId { get; set; }

    public string Name { get; set; } = String.Empty;

}
