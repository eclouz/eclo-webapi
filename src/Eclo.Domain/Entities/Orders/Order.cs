namespace Eclo.Domain.Entities.Orders;

public class Order : Auditable
{
    public long UserId { get; set; }

    public float ProductsPrice { get; set; }

    public string Status { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;

    public bool IsContracted { get; set; } = false;

    public bool IsPaid { get; set; } = false;

    public string PaymentType { get; set; } = String.Empty;
}