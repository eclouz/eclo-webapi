namespace Eclo.WebApi.Configurations.Layers;

public static class WebConfiguration
{
    public static void ConfigureWeb(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(Program));
    }
}