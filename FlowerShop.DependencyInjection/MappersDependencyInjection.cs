using FlowerShop.ApplicationService.Interfaces.Mappers;
using FlowerShop.ApplicationService.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace FlowerShop.DependencyInjection;
internal static class MappersDependencyInjection
{
    internal static void AddMappersDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IFlowerMapper, FlowerMapper>();
        services.AddScoped<IFlowerMapperFacade, FlowerMapper>();

        services.AddScoped<IShopMapper, ShopMapper>();
    }
}
