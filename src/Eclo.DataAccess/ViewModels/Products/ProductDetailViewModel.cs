namespace Eclo.DataAccess.ViewModels.Products;

public class ProductDetailViewModel
{
    public long Id { get; set; } // Product details id

    public long ProductId { get; set; }

    public string ImagePath { get; set; } = String.Empty; // image of each color of cloth

    public string Color { get; set; } = String.Empty;
}
