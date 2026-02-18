using Microsoft.EntityFrameworkCore;
using ServiceRequestApp.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();

// DbContext registration (SQL Server)
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(  
   builder.Configuration.GetConnectionString("DefaultConnection"))  
);

var app = builder.Build();

app.MapGet("/status", () => "OK");
app.Run();
