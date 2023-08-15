using Eclo.DataAccess.Interfaces.Brands;
using Eclo.DataAccess.Interfaces.Categories;
using Eclo.DataAccess.Interfaces.Users;
using Eclo.DataAccess.Repositories.Brands;
using Eclo.DataAccess.Repositories.Categories;
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
    }
}
