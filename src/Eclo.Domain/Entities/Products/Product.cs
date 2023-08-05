using System.ComponentModel.DataAnnotations;

namespace Eclo.Domain.Entities.Products;

public class Product : Auditable
{
    public long BrandId { get; set; }

    public long SubCategoryId { get; set; }

    [MaxLength(50)]
    public string Name { get; set; } = String.Empty;

    public double UnitPrice { get; set; }

    public string Description { get; set; } = String.Empty;

}
