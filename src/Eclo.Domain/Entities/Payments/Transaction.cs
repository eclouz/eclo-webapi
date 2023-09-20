namespace Eclo.Domain.Entities.Payments;

public class Transaction : Auditable
{
    public long UserId { get; set; }

    public string SenderCardNumber { get; set; } = string.Empty;

    public string ReceiverCardNumber { get; set; } = string.Empty;

    public double RequiredAmount { get; set; }

    public bool IsTransfered { get; set; } = false;

    public string Status { get; set; } = String.Empty;
}
