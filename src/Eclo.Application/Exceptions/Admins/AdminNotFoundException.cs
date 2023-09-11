namespace Eclo.Application.Exceptions.Admins;

public class AdminNotFoundException : NotFoundException
{
    public AdminNotFoundException()
    {
        this.TitleMessage = "Admin not found!";
    }
}
