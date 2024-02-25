using Microsoft.Extensions.Configuration;

namespace FlowerShop.Business.Factories;
public static class ConnectionStringFactory
{
    public static string GetConnectionString(this IConfiguration configuration)
    {
        const string dockerEnvironmentValue = "DOCKER_ENV";
        if (Environment.GetEnvironmentVariable("DOCKER_ENVIROMENT") is dockerEnvironmentValue)
            return configuration.GetConnectionString("ContainerConnection")!;

        return configuration.GetConnectionString("LocalConnection")!;
    }
}
