using Eclo.Domain.Entities.Brands;

namespace Eclo.Domain.Entities.Categories;

public class SubCategory : Auditable
{
    public long CategoryId { get; set; }

    public string Name { get; set; } = String.Empty;

    public string CategoryName { get; set; } = String.Empty;
}
