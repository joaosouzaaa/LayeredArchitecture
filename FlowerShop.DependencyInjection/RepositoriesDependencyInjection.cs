using FlowerShop.Infrastructure.Interfaces.Repositories;
using FlowerShop.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FlowerShop.DependencyInjection;
internal static class RepositoriesDependencyInjection
{
    internal static void AddRepositoriesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IFlowerRepository, FlowerRepository>();
        services.AddScoped<IShopRepository, ShopRepository>();
    }
}
