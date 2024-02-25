using FlowerShop.API.DependencyInjection;
using FlowerShop.API.Middlewares;
using FlowerShop.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCorsDependencyInjection();
builder.Services.AddFilterDependencyInjection();
builder.Services.AddDependencyInjection(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseMiddleware<UnexpectedErrorMiddleware>();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
