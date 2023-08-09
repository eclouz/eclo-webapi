namespace Eclo.Persistence.Dtos.Discounts;

public class DiscountCreateDto
{
    public string Name { get; set; } = String.Empty;

    public float Percentage { get; set; }

    public string Description { get; set; } = String.Empty;
}
