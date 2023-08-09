namespace Eclo.Persistence.Dtos.Security;

public class VerificationDto
{
    public int Code { get; set; }

    public int Attempt { get; set; }

    public DateTime CreatedAt { get; set; }
}
