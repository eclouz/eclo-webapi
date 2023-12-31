﻿namespace Eclo.DataAccess.ViewModels.Products;

public class ProductResultViewModel
{

    public long ProductId { get; set; }

    public long ProductDetailId { get; set; }

    public string ProductCategoryName { get; set; } = String.Empty;

    public string ProductName { get; set; } = String.Empty;

    public string ProductImagePath { get; set; } = String.Empty;

    public long ProductLikes { get; set; }

    public double ProductPrice { get; set; }

    public DateTime ProductLastUpdate { get; set; }
}
