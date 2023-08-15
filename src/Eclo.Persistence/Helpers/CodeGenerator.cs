namespace Eclo.Persistence.Helpers;

public class CodeGenerator
{
    public static int GenerateRandomNumber()
    {
        Random random = new Random();
        return random.Next(10000, 99999);
    }
}
