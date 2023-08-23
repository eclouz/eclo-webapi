namespace Eclo.Domain.Entities.Products;

public class ProductDetail : Auditable
{
    public long ProductId { get; set; }

    public string ImagePath { get; set; } = String.Empty;

    public string Color { get; set; } = String.Empty;

    public List<ProductDetailFashion> ProductDetailFashions { get; set; }
        = new List<ProductDetailFashion>();

    public List<ProductDetailSize> ProductDetailSizes { get; set; }
        = new List<ProductDetailSize>();
}