﻿using Microsoft.AspNetCore.Http;

namespace Eclo.Persistence.Dtos.Products;

public class ProductDetailCreateDto
{
    public long ProductId { get; set; }

    public IFormFile ImagePath { get; set; } = default!;

    public string Color { get; set; } = String.Empty;
}
