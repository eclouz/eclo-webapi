using Eclo.Domain.Entities.Brands;
using Eclo.Domain.Entities.Categories;
using Eclo.Domain.Entities.Discounts;
using Eclo.Domain.Entities.Products;
using Eclo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclo.DataAccess.ViewModels.Products;

public class ProductGetViewModels : Auditable
{
    public string ProductName { get; set; } = String.Empty;

    public long BrandId { get; set; }

    public List<Brand> Brand { get; set; }
        = new List<Brand>();

    public List<ProductDetail> ProductDetail { get; set; }
        = new List<ProductDetail>();

    public double ProductPrice { get; set; }

    public string ProductDescription { get; set; } = String.Empty;

    public long SubCategoryId { get; set; }

    public List<SubCategory> SubCategory { get; set; }
        = new List<SubCategory>();

    public List<float> ProductDiscount { get; set; }
        = new List<float>();

    public bool ProductLiked { get; set; } = false;

    public long likedId { get; set; }

    public List<ProductComment> ProductComments { get; set; }
      = new List<ProductComment>();
}
