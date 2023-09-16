namespace Eclo.Application.Exceptions.Admins;

public class AdminAlreadyExistsException : AlreadyExistsException
{
    public AdminAlreadyExistsException()
    {
        this.TitleMessage = "Admin already exists!";
    }
}
