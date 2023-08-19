namespace Eclo.Application.Exceptions.Products;

public class ProductCommentNotFoundException : NotFoundException
{
    public ProductCommentNotFoundException()
    {
        this.TitleMessage = "ProductComment not found!";
    }
}
