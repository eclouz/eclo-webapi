namespace Eclo.Application.Exceptions.Users;

public class UserAlreadyExistsException : AlreadyExistsException
{
    public UserAlreadyExistsException()
    {
        TitleMessage = "User already exists!";
    }
    public UserAlreadyExistsException(string phone)
    {
        TitleMessage = "This phone is already registered!";
    }
}
