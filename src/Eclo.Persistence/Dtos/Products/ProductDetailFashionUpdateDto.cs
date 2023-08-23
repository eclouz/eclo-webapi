using Microsoft.AspNetCore.Http;

namespace Eclo.Persistence.Dtos.Products;

public class ProductDetailFashionUpdateDto
{
    public long ProductDetailId { get; set; }

    public IFormFile? ImagePath { get; set; }
}
