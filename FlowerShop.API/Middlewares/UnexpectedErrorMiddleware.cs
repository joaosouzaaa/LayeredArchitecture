using FlowerShop.CrossCutting.Settings.NotificationSettings;
using System.Net.Mime;
using System.Text.Json;

namespace FlowerShop.API.Middlewares;

public sealed class UnexpectedErrorMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = MediaTypeNames.Application.Json;

            var response = new List<Notification>()
            {
                new()
                {
                    Key = "Unexpected Error",
                    Message = "An unexpected error happened."
                }
            };

            var responseJsonString = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(responseJsonString);
        }
    }
}
