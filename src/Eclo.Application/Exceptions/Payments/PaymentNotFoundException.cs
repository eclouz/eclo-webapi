namespace Eclo.Application.Exceptions.Payments;

public class PaymentNotFoundException : NotFoundException
{
    public PaymentNotFoundException()
    {
        this.TitleMessage = "Payment not found!";
    }
}