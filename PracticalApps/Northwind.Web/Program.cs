using Northwind.Web;

Host.CreateDefaultBuilder(args)
.ConfigureWebHostDefaults(builder =>
{
    builder.UseStartup<Startup>();
}).Build().Run();

/*
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapGet("/", () => "Hello World!");

app.Run();
*/

Console.WriteLine("Web server execution is completed!");