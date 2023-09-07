namespace Eclo.Domain.Entities.Payments;

public class Payment : Auditable
{
    public long OrderDetailId { get; set; }

    public string TransactionStatus { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;
}