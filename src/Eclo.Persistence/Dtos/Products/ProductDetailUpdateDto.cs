using Microsoft.AspNetCore.Http;

namespace Eclo.Persistence.Dtos.Products;

public class ProductDetailUpdateDto
{
    public long ProductId { get; set; }

    public IFormFile? ImagePath { get; set; }

    public string Color { get; set; } = String.Empty;
}
