namespace Eclo.Application.Exceptions.Brands;

public class BrandNotFoundException : NotFoundException
{
    public BrandNotFoundException()
    {
        this.TitleMessage = "Brand not found!";
    }
}
