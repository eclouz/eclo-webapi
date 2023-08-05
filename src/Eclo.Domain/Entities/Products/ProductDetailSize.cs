namespace Eclo.Domain.Entities.Products;

public class ProductDetailSize : Auditable
{
    public long ProductDetailId { get; set; }

    public string Size { get; set; } = String.Empty;

    public int Quantity { get; set; }
}

