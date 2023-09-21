namespace Eclo.Persistence.Dtos.Payments;

public class TransactionCreateDto
{
    public string SenderCardNumber { get; set; } = String.Empty;

    //public string ReceiverCardNumber { get; set; } = String.Empty;

    public double RequiredAmount { get; set; }

    //public bool IsTransfered { get; set; } = true;

    //public string Status { get; set; } = String.Empty;
}
