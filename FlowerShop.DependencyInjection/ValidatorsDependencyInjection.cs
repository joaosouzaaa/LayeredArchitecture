using FlowerShop.CrossCutting.Settings.ValidatorSettings;
using FlowerShop.Domain.Entites;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FlowerShop.DependencyInjection;
internal static class ValidatorsDependencyInjection
{
    internal static void AddValidatorsDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IValidator<Flower>, FlowerValidator>();
        services.AddScoped<IValidator<Shop>, ShopValidator>();
    }
}
