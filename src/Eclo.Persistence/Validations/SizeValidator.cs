namespace Eclo.Persistence.Validations;

public class SizeValidator
{
    public static bool IsValid(string size)
    {
        try
        {
            if (size.Length == 0 || size.Length > 6) return false;

            if (size == "S" || size == "M" || size == "L" || size == "XL/M" || size == "XL/L" || size == "XL" ||
                size == "XXL/M" || size == "XXL/L" || size == "XXL" || size == "XXXL/M" || size == "X" || size == "XXXL/M" || size == "XXXL/L" ||
                size == "XXXL/XL" || size == "XXXL" || size == "XXXXL" || size == "XXX" || size == "XXXXXL") return true;

            int s;

            s = int.Parse(size);

            if (s > 0 && s < 250) return true;
        }
        catch
        {
            return false;
        }
        return false;
    }
}
