using MediatR;
using NLog.Web;
using ProductsManager.API.Middlewares;
using ProductsManager.Application;
using ProductsManager.Application.Products.Commands.Create;
using ProductsManager.Application.Products.Commands.Update;
using ProductsManager.Application.Products.Queries.GetAll;
using ProductsManager.Application.Products.Queries.GetById;
using ProductsManager.Infrastructure.Persistence;
using ProductsManager.Infrastructure.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationLayer();
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddSharedInfrastructure();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseNLog();

var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var dbInitializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
    if (dbInitializer != null)
    {
        await dbInitializer.InitializeAsync();
    }
}

app.UseHttpsRedirection();

app.MapPost("/products", async (CreateProductCommand command, IMediator mediator) =>
{
    await mediator.Send(command);
})
.WithName("SaveProduct")
.WithOpenApi();

app.MapGet("/products", async (IMediator mediator) =>
{
    var response = await mediator.Send(new GetProductsQuery());
    return Results.Ok(response);
})
.WithName("GetProducts")
.WithOpenApi();

app.MapGet("/products/{id}", async (int id,IMediator mediator) =>
{
    var response = await mediator.Send(new GetProductByIdQuery() { Id = id});
    return Results.Ok(response);
})
.WithName("GetProductById")
.WithOpenApi();

app.MapPut("/products", async (UpdateProductCommand command, IMediator mediator) =>
{
    var response = await mediator.Send(command);    
})
.WithName("UpdateProduct")
.WithOpenApi();

app.Run();
