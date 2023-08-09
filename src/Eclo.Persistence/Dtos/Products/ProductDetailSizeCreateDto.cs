namespace Eclo.Persistence.Dtos.Products;

public class ProductDetailSizeCreateDto
{
    public long ProductDetailId { get; set; }

    public string Size { get; set; } = String.Empty;

    public int Quantity { get; set; }
}
