using Eclo.Services.Interfaces.Auth;
using Eclo.Services.Interfaces.Brands;
using Eclo.Services.Interfaces.Categories;
using Eclo.Services.Interfaces.Common;
using Eclo.Services.Interfaces.Notifications;
using Eclo.Services.Services.Auth;
using Eclo.Services.Services.Brands;
using Eclo.Services.Services.Categories;
using Eclo.Services.Services.Common;
using Eclo.Services.Services.Notifications;

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
        builder.Services.AddScoped<IBrandService, BrandService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddSingleton<ISmsSender, SmsSender>();
    }
}
