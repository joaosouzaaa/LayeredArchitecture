using FlowerShop.ApplicationService.Interfaces.Services;
using FlowerShop.ApplicationService.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FlowerShop.DependencyInjection;
internal static class ServicesDependencyInjection
{
    internal static void AddServicesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IFlowerService, FlowerService>();
    }
}
