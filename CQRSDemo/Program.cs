using CQRSDemo.Interfaces;
using CQRSDemo.Models;
using CQRSDemo.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IProductContext>(new ProductContext(builder.Configuration.GetConnectionString("DBDemo")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseExceptionHandler("/oops");
}

app.MapGet("/oops", () => Results.Problem());

string ExceptionMessage = "";
app.MapPost("/Products", async ([FromServices] IProductContext context) =>
{
    context.ExceptionEvent += Context_ExceptionEvent;
    var datos = await context.Create(new CQRSDemo.Models.Product() { Name = "Azúcar", Discontinued = false, Quantity = 0, UnitPrice = 10 });
    context.ExceptionEvent -= Context_ExceptionEvent;

    return string.IsNullOrWhiteSpace(ExceptionMessage) ? Results.Ok(ValueTask.FromResult(datos)) : Results.Problem(ExceptionMessage, statusCode:400);
});

app.MapGet("/Products", async ([FromServices] IProductContext context) =>
{
    return await context.Retrieve();
});

app.MapGet("/Products/{id}", async ([FromServices] IProductContext context, int id) =>
{
    return await context.Retrieve(id);
});

app.MapPut("/Products", async ([FromServices] IProductContext context, Product product) =>
{
    return await context.Update(product);
});

app.MapMethods("/Products/{id}", new[] { "PATCH" }, async ([FromServices] IProductContext context, int id, string propertyName, string value) =>
{
    return await context.Update(id, propertyName, value);
});

app.MapDelete("/Products/{id}", async ([FromServices] IProductContext context, int id) =>
{
    return await context.Delete(id);
});

void Context_ExceptionEvent(object? sender, ExceptionEventArgs e)
{
    ExceptionMessage = e.Exception.Message;
}

app.Run();