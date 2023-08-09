namespace Eclo.Persistence.Dtos.Products;

public class UserProductLikeCreateDto
{
    public long UserId { get; set; }

    public long ProductId { get; set; }

    public bool IsLiked { get; set; } = false;
}
