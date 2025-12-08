using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Config;
using Infrastructure.Repositories;
using Infrastructure.Service.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddPersistence(
		this IServiceCollection services)
	{
        services.AddDbContext<AppDbContext>(options =>
			options.UseInMemoryDatabase("ShoppingAppDb"));

        services.AddScoped<IItemRepository, ItemRepository>();

		services.AddScoped<IPurchaseRepository, PurchaseRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();


        return services;
	}
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddSingleton<ITopCategoryCalculator, TopCategoryConfigProvider>();

        services.AddSingleton<IOptionsMonitor<TopCategoryConfig>>(sp =>
            sp.GetRequiredService<ITopCategoryCalculator>() as IOptionsMonitor<TopCategoryConfig>
            ?? throw new InvalidOperationException("Config provider not initialized"));

        return services;
    }
}
