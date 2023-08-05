namespace Eclo.Domain.Entities.Products;

public class ProductDetailFashion : Auditable
{
    public long ProductDetailId { get; set; }

    public string ImagePath { get; set; } = String.Empty;
}
