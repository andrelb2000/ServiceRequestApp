var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/status", () => "OK");

app.Run();
