namespace Eclo.Application.Exceptions.Orders;

public class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException()
    {
        this.TitleMessage = "Order not found!";
    }
}