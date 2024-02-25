using FlowerShop.Business.Interfaces.Settings;
using FlowerShop.Business.Settings.NotificationSettings;
using Microsoft.Extensions.DependencyInjection;

namespace FlowerShop.DependencyInjection;

internal static class SettingsDependencyInjection
{
    internal static void AddSettingsDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<INotificationHandler, NotificationHandler>();
    }
}
