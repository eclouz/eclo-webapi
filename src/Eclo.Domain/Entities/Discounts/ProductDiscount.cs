namespace Eclo.Domain.Entities.Discounts;

public class ProductDiscount : Discount
{
    public long ProductId { get; set; }

    public long DiscountId { get; set; }

    //public string Description { get; set; } = String.Empty;

    public DateTime StartAt { get; set; }

    public DateTime EndAt { get; set; }

    public string ProductDescription { get; set; } = String.Empty;
}
