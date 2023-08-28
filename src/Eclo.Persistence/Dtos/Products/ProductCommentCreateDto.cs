namespace Eclo.Persistence.Dtos.Products;

public class ProductCommentCreateDto
{
    public long ProductId { get; set; }

    public long UserId { get; set; }

    public string Comment { get; set; } = String.Empty;

    public bool IsEdited { get; set; } = false;
}
