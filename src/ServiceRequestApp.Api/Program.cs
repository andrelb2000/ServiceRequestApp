using Microsoft.EntityFrameworkCore;
using ServiceRequestApp.Api.Endpoints;
using ServiceRequestApp.Application.Interfaces;
using ServiceRequestApp.Infrastructure.Data;
using ServiceRequestApp.Infrastructure.Repositories;
using ServiceRequestApp.Service.Services;
using ServiceRequestApp.Api.Hubs;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
// DbContext — Scoped by default via AddDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
 options.UseSqlServer( builder.Configuration
                       .GetConnectionString("DefaultConnection"))
);
// DI registrations — all Scoped
builder.Services.AddScoped<IServiceRequestRepository,
                          ServiceRequestRepository>();
builder.Services.AddScoped<IServiceRequestService,
                          ServiceRequestService>();
////////////////// Blazer adding for CORS /////
// In ServiceRequestApp.Api/Program.cs
// Change FOR signalr
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => {
        policy.WithOrigins("http://localhost:5120")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

////////////////// SignalR for real-time updates ///
builder.Services.AddSignalR(); // Add SignalR services for real-time communication
// Register the SignalR implementation of IRealtimeNotifier.
// Scoped lifetime is correct here because:
//  - IHubContext<T> is itself registered as Singleton by AddSignalR()
//  - Scoped services CAN depend on Singleton services safely
//  - Scoped aligns with the lifetime of ServiceRequestService
builder.Services.AddScoped<ServiceRequestApp.Application.Interfaces.IRealtimeNotifier,
                           ServiceRequestApp.Api.Realtime.SignalRRealtimeNotifier>();
var app = builder.Build();
// Before app.UseAuthorization():
app.UseCors();
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
app.MapServiceRequestEndpoints();

app.MapHub<ServiceRequestHub>("/hubs/servicerequests"); // Map SignalR hub for real-time updates

app.Run();