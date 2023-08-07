namespace Eclo.DataAccess.ViewModels.Products;

public class ProductCommentViewModel
{
    public int Id { get; set; }

    public string Comment { get; set; } = String.Empty;

    public DateTime CreatedAt { get; set; }

    public string FirstName { get; set; } = String.Empty;

    public string LastName { get; set; } = String.Empty;

    public string ImagePath { get; set; } = String.Empty;
}
