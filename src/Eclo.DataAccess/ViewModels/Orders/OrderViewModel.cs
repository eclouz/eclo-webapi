namespace Eclo.DataAccess.ViewModels.Orders;

public class OrderViewModel
{
    public long OrderId { get; set; }

    public string ProductName { get; set; } = String.Empty;

    public double ProductPrice { get; set; }

    public Int16 DiscountPercentage { get; set; }

    public int Quantity { get; set; }

    public double TotalPrice { get; set; }

    public bool IsContracted { get; set; }

    public bool IsPaid { get; set; }

    public string OrderStatus { get; set; } = String.Empty;
}