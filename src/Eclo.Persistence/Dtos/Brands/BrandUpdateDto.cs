using Microsoft.AspNetCore.Http;

namespace Eclo.Persistence.Dtos.Brands;

public class BrandUpdateDto
{
    public string Name { get; set; } = String.Empty;

    public IFormFile BrandIconPath { get; set; } = default!;
}
