using Eclo.Domain.Entities;
using Eclo.Domain.Entities.Brands;
using Eclo.Domain.Entities.Categories;
using Eclo.Domain.Entities.Discounts;
using Eclo.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclo.DataAccess.ViewModels.Products;

public class ProductGetViewModel : Auditable
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

    public List<ProductDiscount> ProductDiscount { get; set; }
        = new List<ProductDiscount>();

    public bool ProductLiked { get; set; } = false;

    public long likedId { get; set; }

    public List<ProductComment> ProductComments { get; set; }
      = new List<ProductComment>();

}
