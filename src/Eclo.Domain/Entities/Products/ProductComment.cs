namespace Eclo.Domain.Entities.Products;

public class ProductComment : Auditable
{
    public long ProductId { get; set; }

    public long UserId { get; set; }

    public long ReplyCommentId { get; set; }

    public string Comment { get; set; } = String.Empty;

    public bool IsEdited { get; set; } = false;
}
