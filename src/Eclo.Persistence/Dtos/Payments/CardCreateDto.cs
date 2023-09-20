namespace Eclo.Persistence.Dtos.Payments;

public class CardCreateDto
{
    public long UserId { get; set; }

    public string CardHolderName { get; set; } = String.Empty;

    public string CardNumber { get; set; } = String.Empty;

    public double Balance { get; set; }

    public int PinCode { get; set; }

    public int ExpiredMonth { get; set; }

    public int ExpiredYear { get; set; }

    public bool IsActive { get; set; }
}
