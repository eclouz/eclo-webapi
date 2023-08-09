using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclo.Persistence.Dtos.Products;

public class ProductDetailUpdateDto
{
    public long ProductId { get; set; }

    public IFormFile ImagePath { get; set; } = default!;

    public string Color { get; set; } = String.Empty;
}
