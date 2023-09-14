namespace Eclo.Domain.Entities.Orders;

public class OrderDetail : Auditable
{
    public long OrderId { get; set; }

    public long ProductDiscountId { get; set; }

    public int Quantity { get; set; }

    public double Price { get; set; }

    public double DiscountPrice { get; set; }

    public double TotalPrice { get; set; }
}