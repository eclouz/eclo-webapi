using Eclo.Application.Exceptions.Products;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Brands;
using Eclo.DataAccess.Interfaces.Categories;
using Eclo.DataAccess.Interfaces.Discounts;
using Eclo.DataAccess.Interfaces.Products;
using Eclo.DataAccess.ViewModels.Products;
using Eclo.Domain.Entities.Categories;
using Eclo.Domain.Entities.Discounts;
using Eclo.Domain.Entities.Products;
using Eclo.Persistence.Dtos.Products;
using Eclo.Persistence.Helpers;
using Eclo.Services.Interfaces.Common;
using Eclo.Services.Interfaces.Products;

namespace Eclo.Services.Services.Products;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IBrandRepository _brandRepository;
    private readonly IProductDetailRepository _productDetailRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IProductDetailFashionRepository _productDetailFashionRepository;
    private readonly IProductDetailSizeRepository _productDetailSizeRepository;
    private readonly ISubCategoryRepository _subCategoryRepository;
    private readonly IProductCommentRepository _productCommentRepository;
    private readonly IProductDiscountRepository _productDiscountRepository;
    private readonly ICategoryRepository _categoryRepository;

    private readonly IPaginator _paginator;

    public ProductService(IProductRepository productRepository,
        IPaginator paginator, IBrandRepository brandRepository,
        IProductDetailRepository productDetailRepository,
        IDiscountRepository discountRepository, 
        IProductDiscountRepository productDiscountRepository,
        IProductDetailFashionRepository productDetailFashionRepository,
        IProductDetailSizeRepository productDetailSizeRepository,
        ISubCategoryRepository subCategoryRepository,
        IProductCommentRepository productCommentRepository,
        ICategoryRepository categoryRepository)
    {
        this._repository = productRepository;
        this._paginator = paginator;
        this._brandRepository = brandRepository;
        this._productDetailRepository = productDetailRepository;
        this._discountRepository = discountRepository;
        this._productDetailFashionRepository = productDetailFashionRepository;
        this._productDetailSizeRepository = productDetailSizeRepository;
        this._productCommentRepository = productCommentRepository;
        this._subCategoryRepository = subCategoryRepository;
        this._productDiscountRepository = productDiscountRepository;
        this._categoryRepository = categoryRepository;
    }

    public async Task<long> CountAsync() => await _repository.CountAsync();

    public async Task<bool> CreateAsync(ProductCreateDto dto)
    {
        Product product = new Product()
        {
            BrandId = dto.BrandId,
            SubCategoryId = dto.SubCategoryId,
            Name = dto.Name,
            UnitPrice = dto.UnitPrice,
            Description = dto.Description,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };

        var result = await _repository.CreateAsync(product);
        
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long productId)
    {
        var product = await _repository.GetById(productId);
        if (product == null) throw new ProductNotFoundException();

        var dbResult = await _repository.DeleteAsync(productId);

        return dbResult > 0;
    }

    public async Task<IList<ProductGetViewModel>> FiltrAsync(string category, int min, int max, List<string> subCategories, PaginationParams @params)
    {
        var product = await GetAllView(@params);

        List<ProductGetViewModel> list = new List<ProductGetViewModel>();

        if (category != "" && min==0 && max==0 && subCategories.Count==0)
        {
            for (int i = 0; i < product.Count; i++)
            {
                if (category == product[i].SubCategory[0].CategoryName)
                {
                    list.Add(product[i]);
                }
            }
        }

        else if (category != "" && min <= max && min>0 && subCategories.Count == 0)
        {
            for (int i = 0; i < product.Count; i++)
            {
                if (category == product[i].SubCategory[0].CategoryName && 
                    product[i].ProductPrice >= min && product[i].ProductPrice <= max)
                {
                    list.Add(product[i]);
                }
            }
        }

        else if (category != "" && min == 0 && max == 0 && subCategories.Count > 0)
        {
            for (int j = 0; j < subCategories.Count; j++)
            {
                for (int i = 0; i < product.Count; i++)
                {
                    if (category == product[i].SubCategory[0].CategoryName && 
                        subCategories[j] == product[i].SubCategory[0].Name)
                    {
                        list.Add(product[i]);
                    }
                }
            }
        }

        else if (category != "" && min <= max && min > 0 && subCategories.Count > 0)
        {
            for (int j = 0; j < subCategories.Count; j++)
            {
                for (int i = 0; i < product.Count; i++)
                {
                    if (category == product[i].SubCategory[0].CategoryName &&
                        subCategories[j] == product[i].SubCategory[0].Name &&
                        product[i].ProductPrice >= min && product[i].ProductPrice <= max)
                    {
                        list.Add(product[i]);
                    }
                }
            }
        }

        //var count = list.Count;
        //_paginator.Paginate(count, @params);
        return list;
    }

    public async Task<IList<Product>> GetAllAsync(PaginationParams @params)
    {
        var products = await _repository.GetAllAsync(@params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);

        return products;
    }

    public async Task<IList<ProductGetViewModel>> GetAllView(PaginationParams @params)
    {
        var product = await _repository.GetAllAsync(@params);
        var brand = await _brandRepository.GetAllAsync(@params);
        var productDetail = await _productDetailRepository.GetAllAsync(@params);
        var productDiscounts = await _productDiscountRepository.GetAllAsync(@params);
        var discounts = await _discountRepository.GetAllAsync(@params);
        var productDetailFashion = await _productDetailFashionRepository.GetAllAsync(@params);
        var productDetailSize = await _productDetailSizeRepository.GetAllAsync(@params);
        var subCategory = await _subCategoryRepository.GetAllAsync(@params);
        var productComment = await _productCommentRepository.GetAllAsync(@params);
        var category = await _categoryRepository.GetAllAsync(@params);


        List<ProductGetViewModel> list = new List<ProductGetViewModel>();
        for (int m = 0; m < product.Count; m++)
        {
            ProductGetViewModel productGetViewModel = new ProductGetViewModel();
            ProductDetail productDetails = new ProductDetail();
            ProductDiscount productDiscount = new ProductDiscount();
            SubCategory subCategory1 = new SubCategory();

            productGetViewModel.Id = product[m].Id;
            productGetViewModel.ProductName = product[m].Name;
            productGetViewModel.ProductPrice = product[m].UnitPrice;
            productGetViewModel.BrandId = product[m].BrandId;
            productGetViewModel.SubCategoryId = product[m].SubCategoryId;
            productGetViewModel.CreatedAt = product[m].CreatedAt;
            productGetViewModel.UpdatedAt = product[m].UpdatedAt;

            for (int j = 0; j < brand.Count; j++)
            {
                if (productGetViewModel.BrandId == brand[j].Id)
                {
                    productGetViewModel.Brand.Add(brand[j]);
                }
            }

            for (int j = 0; j < productDetail.Count; j++)
            {
                if (productGetViewModel.Id == productDetail[j].ProductId)
                {
                    productDetails.Id = productDetail[j].Id;
                    productDetails.ImagePath = productDetail[j].ImagePath;
                    productDetails.Color = productDetail[j].Color;
                    productDetails.CreatedAt = productDetail[j].CreatedAt;
                    productDetails.UpdatedAt = productDetail[j].UpdatedAt;
                    productDetails.ProductId = productDetail[j].ProductId;
                    for (int i = 0; i < productDetailFashion.Count; i++)
                    {
                        if (productDetails.Id == productDetailFashion[i].ProductDetailId)
                        {
                            productDetails.ProductDetailFashions.Add(productDetailFashion[i]);
                        }
                    }

                    for (int i = 0; i < productDetailSize.Count; i++)
                    {
                        if (productDetails.Id == productDetailSize[i].ProductDetailId)
                        {
                            productDetails.ProductDetailSizes.Add(productDetailSize[i]);
                        }
                    }
                    productGetViewModel.ProductDetail.Add(productDetails);
                }
            }

            for (int j = 0; j < productDiscounts.Count; j++)
            {
                if (productGetViewModel.Id == productDiscounts[j].ProductId)
                {
                    productDiscount.Id = productDiscounts[j].Id;
                    productDiscount.Description = productDiscounts[j].Description;
                    productDiscount.StartAt = productDiscounts[j].StartAt;
                    productDiscount.EndAt = productDiscounts[j].EndAt;
                    productDiscount.ProductId = productDiscounts[j].ProductId;
                    productDiscount.DiscountId = productDiscounts[j].DiscountId;
                    for (int k = 0; k < discounts.Count; k++)
                    {
                        if (productDiscounts[j].ProductId == discounts[k].Id)
                        {
                            productDiscount.ProductDescription = discounts[k].Description;
                            productDiscount.Percentage = discounts[k].Percentage;
                        }
                    }
                    productGetViewModel.ProductDiscount.Add(productDiscount);
                }
            }

            for (int j = 0; j < subCategory.Count; j++)
            {
                if (productGetViewModel.SubCategoryId == subCategory[j].Id)
                {
                    subCategory1.Id = subCategory[j].Id;
                    subCategory1.Name = subCategory[j].Name;
                    subCategory1.CategoryId = subCategory[j].CategoryId;
                    subCategory1.CreatedAt = subCategory[j].CreatedAt;
                    subCategory1.UpdatedAt = subCategory[j].UpdatedAt;

                    for (int k = 0; k < category.Count; k++)
                    {
                        if (subCategory1.CategoryId == category[k].Id)
                        {
                            subCategory1.CategoryName = category[k].Name;
                        }
                    }
                    productGetViewModel.SubCategory.Add(subCategory1);
                }
            }

            for (int j = 0; j < productComment.Count; j++)
            {
                if (productGetViewModel.Id == productComment[j].ProductId)
                {
                    productGetViewModel.ProductComments.Add(productComment[j]);
                }
            }
            list.Add(productGetViewModel);
        }

        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);
        return list;
    }

    public async Task<IList<ProductViewModel>> GetAllViewAsync(PaginationParams @params)
    {
        var product = await _repository.GetAllAsync(@params);
        var brand = await _brandRepository.GetAllAsync(@params);
        var productDetail = await _productDetailRepository.GetAllAsync(@params);
        var productDiscounts = await _productDiscountRepository.GetAllAsync(@params);
        var discounts = await _discountRepository.GetAllAsync(@params);

        List<ProductViewModel> list = new List<ProductViewModel>();
        for (int i = 0; i < product.Count; i++)
        {
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.Id = product[i].Id;
            productViewModel.ProductName = product[i].Name;
            productViewModel.ProductPrice = product[i].UnitPrice;
            productViewModel.BrandId = product[i].BrandId;
            for (int j = 0; j < brand.Count; j++)
            {
                if (productViewModel.BrandId == brand[j].Id)
                {
                    productViewModel.Brand.Add(brand[j]);
                }
            }
            for (int j = 0; j < productDetail.Count; j++)
            {
                if (productViewModel.Id == productDetail[j].ProductId)
                {
                    productViewModel.ProductDetail.Add(productDetail[j]);
                }
            }
            for (int j = 0; j < productDiscounts.Count; j++)
            {
                if (productViewModel.Id == productDiscounts[j].ProductId)
                {
                    long productDiscountId = productDiscounts[j].DiscountId;
                    for (int k = 0; k < discounts.Count; k++)
                    {
                        if(productDiscountId == discounts[k].Id)
                        {
                            productViewModel.ProductDiscount.Add(discounts[k].Percentage);
                        }
                    }
                }
            }
            list.Add(productViewModel);
        }

        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);
        return list;
    }

    public async Task<Product> GetByIdAsync(long productId)
    {
        var product = await _repository.GetByIdAsync(productId);
        if (product == null) throw new ProductNotFoundException();
        else return product; 
    }

    public async Task<ProductGetViewModel> GetByIdViewAsync(long productId, PaginationParams @params)
    {
        var product = await _repository.GetByIdAsync(productId);
        var brand = await _brandRepository.GetAllAsync(@params);
        var productDetail = await _productDetailRepository.GetAllAsync(@params);
        var productDiscounts = await _productDiscountRepository.GetAllAsync(@params);
        var discounts = await _discountRepository.GetAllAsync(@params);
        var productDetailFashion = await _productDetailFashionRepository.GetAllAsync(@params);
        var productDetailSize = await _productDetailSizeRepository.GetAllAsync(@params);
        var subCategory = await _subCategoryRepository.GetAllAsync(@params);
        var productComment = await _productCommentRepository.GetAllAsync(@params);
        var category = await _categoryRepository.GetAllAsync(@params);

        ProductGetViewModel productGetViewModel = new ProductGetViewModel();
        ProductDetail productDetails = new ProductDetail();
        ProductDiscount productDiscount = new ProductDiscount();
        SubCategory subCategory1 = new SubCategory();

        productGetViewModel.Id = product.Id;
        productGetViewModel.ProductName = product.Name;
        productGetViewModel.ProductPrice = product.UnitPrice;
        productGetViewModel.BrandId = product.BrandId;
        productGetViewModel.SubCategoryId = product.SubCategoryId;
        productGetViewModel.CreatedAt = product.CreatedAt;
        productGetViewModel.UpdatedAt = product.UpdatedAt;
        
        for (int j = 0; j < brand.Count; j++)
        {
            if (productGetViewModel.BrandId == brand[j].Id)
            {
                productGetViewModel.Brand.Add(brand[j]);
            }
        }
        
        for (int j = 0; j < productDetail.Count; j++)
        {
            if (productGetViewModel.Id == productDetail[j].ProductId)
            {
                productDetails.Id = productDetail[j].Id;
                productDetails.ImagePath = productDetail[j].ImagePath;
                productDetails.Color = productDetail[j].Color;
                productDetails.CreatedAt = productDetail[j].CreatedAt;
                productDetails.UpdatedAt = productDetail[j].UpdatedAt;
                productDetails.ProductId = productDetail[j].ProductId;
                for (int i = 0; i < productDetailFashion.Count; i++)
                {
                    if (productDetails.Id == productDetailFashion[i].ProductDetailId)
                    {
                        productDetails.ProductDetailFashions.Add(productDetailFashion[i]);
                    }
                }

                for (int i = 0; i < productDetailSize.Count; i++)
                {
                    if (productDetails.Id == productDetailSize[i].ProductDetailId)
                    {
                        productDetails.ProductDetailSizes.Add(productDetailSize[i]);
                    }
                }
                productGetViewModel.ProductDetail.Add(productDetails);
            }
        }

        
        for (int j = 0; j < productDiscounts.Count; j++)
        {
            if (productGetViewModel.Id == productDiscounts[j].ProductId)
            {
                productDiscount.Id = productDiscounts[j].Id;
                productDiscount.Description = productDiscounts[j].Description;
                productDiscount.StartAt = productDiscounts[j].StartAt;
                productDiscount.EndAt = productDiscounts[j].EndAt;
                productDiscount.ProductId = productDiscounts[j].ProductId;
                productDiscount.DiscountId = productDiscounts[j].DiscountId;
                for (int k = 0; k < discounts.Count; k++)
                {
                    if (productDiscounts[j].ProductId == discounts[k].Id)
                    {
                        productDiscount.ProductDescription = discounts[k].Description;
                        productDiscount.Percentage = discounts[k].Percentage;
                    }
                }
                productGetViewModel.ProductDiscount.Add(productDiscount);
            }
        }

        for (int j = 0; j < subCategory.Count; j++)
        {
            if (productGetViewModel.SubCategoryId == subCategory[j].Id)
            {
                subCategory1.Id = subCategory[j].Id;
                subCategory1.Name = subCategory[j].Name;
                subCategory1.CategoryId = subCategory[j].CategoryId;
                subCategory1.CreatedAt = subCategory[j].CreatedAt;
                subCategory1.UpdatedAt = subCategory[j].UpdatedAt;
                
                for (int k = 0; k < category.Count; k++)
                {
                    if (subCategory1.CategoryId == category[k].Id)
                    {
                        subCategory1.CategoryName = category[k].Name;
                    }
                }      
                productGetViewModel.SubCategory.Add(subCategory1);
            }
        }

        for (int j = 0; j < productComment.Count; j++)
        {
            if (productGetViewModel.Id == productComment[j].ProductId)
            {
                productGetViewModel.ProductComments.Add(productComment[j]);
            }
        }

        return productGetViewModel;
    }

    public async Task<IList<Product>> SearchAsync(string search, PaginationParams @params)
    {
        var products = await _repository.SearchAsync(search, @params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);

        return products.Item2.ToList(); 
    }

    public async Task<bool> UpdateAsync(long productId, ProductUpdateDto dto)
    {
        var product = await _repository.GetById(productId);
        if (product == null) throw new ProductNotFoundException();

        // update product with new items 
        product.BrandId = dto.BrandId;
        product.SubCategoryId = dto.SubCategoryId;
        product.Name = dto.Name;
        product.UnitPrice = dto.UnitPrice;
        product.Description = dto.Description;
        product.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _repository.UpdateAsync(productId, product);

        return dbResult > 0;
    }
}
