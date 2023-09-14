namespace Eclo.Application.LogicServices;

public class GetTotalPrice
{
    public static double TotalPrice(float discount_percentage, double product_price, int product_quantity)
    {
        var result = DiscountPrice(discount_percentage, product_price) * product_quantity;
        return result;
    }

    public static double DiscountPrice(float discount_percentage, double product_price)
    {
        var result = product_price - product_price * (discount_percentage / 100.0);
        return (int)Math.Ceiling(result);
    }
}
