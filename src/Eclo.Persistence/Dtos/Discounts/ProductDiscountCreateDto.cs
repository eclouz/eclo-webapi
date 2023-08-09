namespace Eclo.Persistence.Dtos.Discounts;

public class ProductDiscountCreateDto
{
    public long ProductId { get; set; }

    public long DiscountId { get; set; }

    public string Description { get; set; } = String.Empty;

    public DateTime StartAt { get; set; }

    public DateTime EndAt { get; set; }
}
