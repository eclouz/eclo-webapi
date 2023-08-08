namespace Eclo.DataAccess.ViewModels.Products;

public class ProductViewModel
{
    public long Id { get; set; }

    public string ProductName { get; set; } = String.Empty;

    public string BrandName { get; set; } = String.Empty;

    public string ProductImagePath { get; set; } = String.Empty;

    public string ProductColor { get; set; } = String.Empty;

    public double ProductPrice { get; set; }

    public int ProductDiscount { get; set; }

    public string ProductDescription { get; set; } = String.Empty;

    public string ProductSize { get; set; } = String.Empty;

    public bool ProductLiked { get; set; } = false;
}
