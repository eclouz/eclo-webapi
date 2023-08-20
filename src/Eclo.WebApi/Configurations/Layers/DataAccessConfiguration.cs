using Eclo.DataAccess.Interfaces.Brands;
using Eclo.DataAccess.Interfaces.Categories;
using Eclo.DataAccess.Interfaces.Products;
using Eclo.DataAccess.Interfaces.Users;
using Eclo.DataAccess.Repositories.Brands;
using Eclo.DataAccess.Repositories.Categories;
using Eclo.DataAccess.Repositories.Products;
using Eclo.DataAccess.Repositories.Users;

namespace Eclo.WebApi.Configurations.Layers;

public static class DataAccessConfiguration
{
    public static void ConfigureDataAccess(this WebApplicationBuilder builder)
    {
        //-> DI containers, IoC containers
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IBrandRepository, BrandRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IProductDetailRepository, ProductDetailRepository>();
        builder.Services.AddScoped<IProductDetailFashionRepository, ProductDetailFashionRepository>();
        builder.Services.AddScoped<IProductCommentRepository, ProductCommentRepository>();
        builder.Services.AddScoped<IAdminUserRepository, AdminUserRepository>();
    }
}
