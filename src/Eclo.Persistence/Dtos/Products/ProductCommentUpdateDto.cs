namespace Eclo.Persistence.Dtos.Products;

public class ProductCommentUpdateDto
{
    public long ProductId { get; set; }

    public long UserId { get; set; }

    public string Comment { get; set; } = String.Empty;

    public bool IsEdited { get; set; } = false;
}
