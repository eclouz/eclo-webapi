namespace Eclo.Domain.Entities.Discounts;

public class Discount : Auditable
{
    public string Name { get; set; } = String.Empty;

    public float Percentage { get; set; }

    public string Description { get; set; } = String.Empty;
}
