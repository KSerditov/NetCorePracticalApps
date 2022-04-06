using Packt.Shared;
using Microsoft.AspNetCore.Mvc.Formatters;
using static System.Console;
using Northwind.WebApi.Repositories;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddNorthwindContext();
builder.Services.AddControllers(o =>
{
    WriteLine("Default output formatters:");
    foreach (IOutputFormatter formatter in o.OutputFormatters)
    {
        WriteLine(formatter.GetType().Name);
    }
}).AddXmlDataContractSerializerFormatters().AddXmlSerializerFormatters();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Northwind Server API Info", Version = "v1" });
});

builder.Services.AddHttpLogging(c =>
{
    c.LoggingFields = HttpLoggingFields.All;
    c.RequestBodyLogLimit = 4096;
    c.ResponseBodyLogLimit = 4096;
});

var app = builder.Build();

app.UseHttpLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Northwind Service API Version 1");
        c.SupportedSubmitMethods(new[] { SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Put, SubmitMethod.Delete });
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
