namespace Eclo.Application.Exceptions.Products;

public class ProductDiscountNotFoundException : NotFoundException
{
    public ProductDiscountNotFoundException()
    {
        this.TitleMessage = "Product discount not found!";
    }
}
