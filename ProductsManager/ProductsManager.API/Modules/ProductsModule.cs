using MediatR;
using Microsoft.OpenApi.Models;
using ProductsManager.Application.Products.Commands.Create;
using ProductsManager.Application.Products.Commands.Update;
using ProductsManager.Application.Products.Queries.GetAll;
using ProductsManager.Application.Products.Queries.GetById;

namespace ProductsManager.API.Modules
{
    public static class ProductsModule
    {
        public static void AddProductsEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductCommand command, IMediator mediator) =>
            {
                await mediator.Send(command);
                Results.Created();
            })
            .WithName("CreateProduct")
            .WithOpenApi(x => new OpenApiOperation(x)
            {
                Summary = "Add a new product",
                Description = "Adds a product with its current price.",
            });

            app.MapGet("/products", async (IMediator mediator) =>
            {
                var response = await mediator.Send(new GetProductsQuery());
                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .WithOpenApi();

            app.MapGet("/products/{id}", async (int id, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetProductByIdQuery() { Id = id });
                return Results.Ok(response);
            })
            .WithName("GetProductById")
            .WithOpenApi();

            app.MapPut("/products", async (UpdateProductCommand command, IMediator mediator) =>
            {
                await mediator.Send(command);
                Results.Ok();
            })
            .WithName("UpdateProduct")
            .WithOpenApi();
        }
    }
}
