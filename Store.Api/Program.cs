using Store.Api.EndPoints.Store;
using Store.Infrastructure.Ioc.Di;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .RegisterDataBase(builder.Configuration)
    .RegisterServices(builder.Configuration)
    .RegisterRepositories()
    .RegisterProviders()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.
            WhenWritingNull;
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapStoreEndPoints();
app.MapAuthorizationEndPoints();

app.Run();