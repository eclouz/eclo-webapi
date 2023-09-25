namespace Eclo.Application.Exceptions.Payments;

public class CardExpiredException : ExpiredException
{
    public CardExpiredException()
    {
        this.TitleMessage = "The card has expired!";
    }
}
