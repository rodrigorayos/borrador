using Store.Aplication.Services.Store;
using Store.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Store;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<StoreService>(); // Registrar el servicio

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/stores", async (StoreService storeService) =>
{
    var stores = await storeService.GetAllStoresAsync();
    return Results.Ok(stores);
});

app.MapGet("/stores/{id}", async (int id, StoreService storeService) =>
{
    var store = await storeService.GetStoreByIdAsync(id);
    return store != null ? Results.Ok(store) : Results.NotFound();
});

app.MapPost("/stores", async (StoreDto storeDto, StoreService storeService) =>
{
    var store = await storeService.AddStoreAsync(storeDto);
    return Results.Created($"/stores/{store.Id}", store);
});

app.MapPut("/stores/{id}", async (int id, StoreDto storeDto, StoreService storeService) =>
{
    if (id != storeDto.Id)
    {
        return Results.BadRequest("ID mismatch");
    }

    var store = await storeService.UpdateStoreAsync(storeDto);
    return Results.Ok(store);
});

app.MapDelete("/stores/{id}", async (int id, StoreService storeService) =>
{
    var store = await storeService.DeleteStoreAsync(id);
    return store != null ? Results.NoContent() : Results.NotFound();
});

app.Run();