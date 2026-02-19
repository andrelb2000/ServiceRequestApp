using ServiceRequestApp.Application.DTOs;
using ServiceRequestApp.Application.Interfaces;

namespace ServiceRequestApp.Api.Endpoints;
public static class ServiceRequestEndpoints
{
    public static void MapServiceRequestEndpoints(
        this WebApplication app) {
            app.MapGet("/api/servicerequests", async (IServiceRequestService service) => {
                var all = await service.GetAllAsync();
                return Results.Ok(all);
            });

            app.MapGet("/api/servicerequests/{id:int}", async (int id, IServiceRequestService service) =>  {
                    var item = await service.GetByIdAsync(id);
                    return item == null? Results.NotFound(): Results.Ok(item);
            });

            app.MapPost("/api/servicerequests", async (CreateServiceRequestDto dto, IServiceRequestService service) =>{
                    var (ok, error, created) =
                    await service.CreateAsync(dto);
                    if (!ok) return Results.BadRequest(new { error });
                    return Results.Created($"/api/servicerequests/{created!.Id}",created);
            });

            app.MapPut("/api/servicerequests/{id:int}", async (int id, UpdateServiceRequestDto dto, IServiceRequestService service) =>{
                    var (ok, error, updated) = await service.UpdateAsync(id, dto);
                    if (!ok) { 
                        if (error == "Not found.") return Results.NotFound();
                        return Results.BadRequest(new { error });
                    }
                return Results.Ok(updated);
            });

             app.MapDelete("/api/servicerequests/{id:int}", async (int id, IServiceRequestService service) => {
                    var ok = await service.DeleteAsync(id);
                    return ok ? Results.NoContent() : Results.NotFound();
            });
 
    }
}