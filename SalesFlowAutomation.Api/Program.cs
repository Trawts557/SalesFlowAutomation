using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using SalesFlowAutomation.Application.Products.Interfaces;
using SalesFlowAutomation.Application.Sales.Interfaces;
using SalesFlowAutomation.Application.UseCases.Products;
using SalesFlowAutomation.Infrastructure.Persistence;
using SalesFlowAutomation.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();

// Use Cases
builder.Services.AddScoped<GetProductByIdUseCase>();
builder.Services.AddScoped<CreateProductUseCase>();


var app = builder.Build();


app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.Run();


