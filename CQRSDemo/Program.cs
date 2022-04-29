using CQRSDemo.Application.Product.Commands;
using CQRSDemo.Application.Product.Queries;
using CQRSDemo.Interfaces;
using CQRSDemo.Models;
using CQRSDemo.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IProductContext>(new ProductContext(builder.Configuration.GetConnectionString("DBDemo")));

builder.Services.AddMediatR(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseExceptionHandler("/oops");
}

app.MapGet("/oops", () => Results.Problem());

app.MapPost("/Products", async ([FromServices] IMediator mediator, CreateProductCommand command) =>
{
    return await mediator.Send(command);
});

app.MapGet("/Products", async ([FromServices] IMediator mediator) =>
{
    return await mediator.Send(new GetProductsQuery());
});

app.MapGet("/Products/{id}", async ([FromServices] IMediator mediator, int id) =>
{
    return await mediator.Send(new GetProductByIdQuery(id));
});

app.MapPut("/Products", async ([FromServices] IMediator mediator, Product product) =>
{
    return await mediator.Send(new UpdateProductCommand(product));
});

app.MapMethods("/Products/{id}", new[] { "PATCH" }, async ([FromServices] IMediator mediator, int id, string propertyName, string value) =>
{
    return await mediator.Send(new UpdateProductFieldCommand(id, propertyName, value));
});

app.MapDelete("/Products/{id}", async ([FromServices] IMediator mediator, int id) =>
{
    return await mediator.Send(new DeleteProductCommand(id));
});

app.Run();
