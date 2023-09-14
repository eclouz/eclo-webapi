namespace Eclo.Persistence.Dtos.Orders;

public class OrderCreateDto
{
    public long UserId { get; set; }

    public float ProductsPrice { get; set; }

    public string Status { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;

    public bool IsContracted { get; set; }

    public bool IsPaid { get; set; }

    public string PaymentType { get; set; } = String.Empty;
}
