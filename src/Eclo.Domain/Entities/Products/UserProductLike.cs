namespace Eclo.Domain.Entities.Products;

public class UserProductLike : Auditable
{
    public long UserId { get; set; }

    public long ProductId { get; set; }

    public bool IsLiked { get; set; } = false;
}
