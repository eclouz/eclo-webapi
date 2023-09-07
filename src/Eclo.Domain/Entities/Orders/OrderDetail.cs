namespace Eclo.Domain.Entities.Orders;

public class OrderDetail : Auditable
{
    public long OrderId { get; set; }

    public long ProductDiscountId { get; set; }

    public int Quantity { get; set; }

    public float Price { get; set; }

    public float DiscountPrice { get; set; }

    public float TotalPrice { get; set; }
}