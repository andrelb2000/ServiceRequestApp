using Microsoft.EntityFrameworkCore;
using ServiceRequestApp.Api.Endpoints;
using ServiceRequestApp.Application.Interfaces;
using ServiceRequestApp.Infrastructure.Data;
using ServiceRequestApp.Infrastructure.Repositories;
using ServiceRequestApp.Service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

// DbContext — Scoped by default via AddDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
 options.UseSqlServer(
 builder.Configuration
 .GetConnectionString("DefaultConnection"))
);

// DI registrations — all Scoped
builder.Services.AddScoped<IServiceRequestRepository,
                          ServiceRequestRepository>();
builder.Services.AddScoped<IServiceRequestService,
                          ServiceRequestService>();
////////////////// Blazer Tests adding for CORS /////
// In ServiceRequestApp.Api/Program.cs
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy =>  {
         policy.WithOrigins("http://localhost:5120")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});


var app = builder.Build();
// Before app.UseAuthorization():
app.UseCors();

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.MapServiceRequestEndpoints();

app.Run();