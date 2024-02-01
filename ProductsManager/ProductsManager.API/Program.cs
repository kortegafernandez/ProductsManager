using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Models;
using NLog.Web;
using ProductsManager.API.Middlewares;
using ProductsManager.API.Modules;
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

app.AddProductsEndpoints();

app.UseMiddleware<ResponseWrapperMiddleware>();

app.Run();
