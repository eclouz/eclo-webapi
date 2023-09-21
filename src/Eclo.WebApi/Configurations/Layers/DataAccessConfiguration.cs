using Eclo.DataAccess.Interfaces.Admins;
using Eclo.DataAccess.Interfaces.Brands;
using Eclo.DataAccess.Interfaces.Categories;
using Eclo.DataAccess.Interfaces.Discounts;
using Eclo.DataAccess.Interfaces.Heads;
using Eclo.DataAccess.Interfaces.Orders;
using Eclo.DataAccess.Interfaces.Payments;
using Eclo.DataAccess.Interfaces.Products;
using Eclo.DataAccess.Interfaces.Users;
using Eclo.DataAccess.Repositories.Admins;
using Eclo.DataAccess.Repositories.Brands;
using Eclo.DataAccess.Repositories.Categories;
using Eclo.DataAccess.Repositories.Discounts;
using Eclo.DataAccess.Repositories.Heads;
using Eclo.DataAccess.Repositories.Orders;
using Eclo.DataAccess.Repositories.Payments;
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
        builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
        builder.Services.AddScoped<IProductDiscountRepository, ProductDiscountRepository>();
        builder.Services.AddScoped<IUserProductLikeRepository, UserProductLikeRepository>();
        builder.Services.AddScoped<IProductDetailSizeRepository, ProductDetailSizeRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
        builder.Services.AddScoped<IHeadRepository, HeadRepository>();
        builder.Services.AddScoped<IAdminRepository, AdminRepository>();
        builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
        builder.Services.AddScoped<ICardRepository, CardRepository>();
        builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
    }
}
