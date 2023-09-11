using Eclo.Services.Interfaces.Admins;
using Eclo.Services.Interfaces.Auth;
using Eclo.Services.Interfaces.Brands;
using Eclo.Services.Interfaces.Categories;
using Eclo.Services.Interfaces.Common;
using Eclo.Services.Interfaces.Discounts;
using Eclo.Services.Interfaces.Heads;
using Eclo.Services.Interfaces.Notifications;
using Eclo.Services.Interfaces.Orders;
using Eclo.Services.Interfaces.Payments;
using Eclo.Services.Interfaces.Products;
using Eclo.Services.Interfaces.Users;
using Eclo.Services.Services.Admins;
using Eclo.Services.Services.Auth;
using Eclo.Services.Services.Brands;
using Eclo.Services.Services.Categories;
using Eclo.Services.Services.Common;
using Eclo.Services.Services.Discounts;
using Eclo.Services.Services.Heads;
using Eclo.Services.Services.Notifications;
using Eclo.Services.Services.Orders;
using Eclo.Services.Services.Payments;
using Eclo.Services.Services.Products;
using Eclo.Services.Services.Users;

namespace Eclo.WebApi.Configurations.Layers;

public static class ServiceLayerConfiguration
{
    public static void ConfigureServiceLayer(this WebApplicationBuilder builder)
    {
        //-> DI containers, IoC containers
        builder.Services.AddScoped<IFileService, FileService>();
        builder.Services.AddScoped<IPaginator, Paginator>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IIdentityService, IdentityService>();
        builder.Services.AddScoped<IBrandService, BrandService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IProductDetailService, ProductDetailService>();
        builder.Services.AddScoped<IProductDetailFashionService, ProductDetailFashionService>();
        builder.Services.AddScoped<IProductDiscountService, ProductDiscountService>();
        builder.Services.AddScoped<IProductCommentService, ProductCommentService>();
        builder.Services.AddScoped<IProductDetailSizeService, ProductDetailSizeService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUserProductLikeService, UserProductLikeService>();
        builder.Services.AddScoped<IAdminUserService, AdminUserService>();
        builder.Services.AddScoped<IDiscountService, DiscountService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
        builder.Services.AddScoped<IPaymentService, PaymentService>();
        builder.Services.AddScoped<IHeadService, HeadService>();
        builder.Services.AddScoped<IAdminService, AdminService>();
        builder.Services.AddSingleton<ISmsSender, SmsSender>();
    }
}
