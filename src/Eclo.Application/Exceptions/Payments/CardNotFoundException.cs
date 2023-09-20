namespace Eclo.Application.Exceptions.Payments;

public class CardNotFoundException : NotFoundException
{
    public CardNotFoundException()
    {
        this.TitleMessage = "Card not found!";
    }
}
