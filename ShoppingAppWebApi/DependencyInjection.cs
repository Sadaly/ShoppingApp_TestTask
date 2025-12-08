using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Config;
using Microsoft.Extensions.Options;

namespace ShoppingAppWebApi;

public static class DependencyInjection
{
    public static WebApplication AddServiceConfig(
        this WebApplication app)
    {
        app.MapGet("/api/config", (IOptionsMonitor<TopCategoryConfig> config) =>
            Results.Json(config.CurrentValue));

        return app;
    }
    public static IServiceCollection AddContorollersOptions(
        this IServiceCollection service)
    {
        service.AddControllers()
        .AddJsonOptions(options => {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.Converters.Add(
                 new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
         );
        });
        return service;
    }
}
