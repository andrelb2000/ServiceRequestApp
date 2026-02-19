using ServiceRequestApp.Application.DTOs;
using ServiceRequestApp.Application.Interfaces;
using ServiceRequestApp.Application.Validation;
using ServiceRequestApp.Domain.Entities;
using ServiceRequestApp.Domain.Enums;

namespace ServiceRequestApp.Service.Services;

public class ServiceRequestService: IServiceRequestService{
 private readonly IServiceRequestRepository _repo;

 public ServiceRequestService(
 IServiceRequestRepository repo) => _repo = repo;

 public async Task<List<ServiceRequestDto>> GetAllAsync(){
    var items = await _repo.GetAllAsync();
    return items.Select(ToDto).ToList();
 }

 public async Task<ServiceRequestDto?>
 GetByIdAsync(int id){
 var item = await _repo.GetByIdAsync(id);
 return item == null ? null : ToDto(item);
 }

     public async Task<(bool ok, string error,
        ServiceRequestDto? created)>
        CreateAsync(CreateServiceRequestDto dto) {
        var (ok, error) =
            ServiceRequestValidators.Validate(dto);
        if (!ok) return (false, error, null);

        var entity = new ServiceRequest
        {
            Title       = dto.Title.Trim(),
            Description = dto.Description.Trim(),
            Status      = RequestStatus.Open
            // CreatedAt handled by SQL default
        };

        var created = await _repo.AddAsync(entity);
        return (true, "", ToDto(created));
    }

     public async Task<(bool ok, string error,ServiceRequestDto? updated)>
 UpdateAsync(int id, UpdateServiceRequestDto dto)  {
 var (ok, error) =
 ServiceRequestValidators.Validate(dto);
 if (!ok) return (false, error, null);

 var existing = await _repo.GetByIdAsync(id);
 if (existing == null)
 return (false, "Not found.", null);

 existing.Title = dto.Title.Trim();
 existing.Description = dto.Description.Trim();
 existing.Status = (RequestStatus)dto.Status;

 // Business rule: set CompletedAt exactly once
 if (existing.Status == RequestStatus.Completed && existing.CompletedAt == null)
        existing.CompletedAt = DateTime.UtcNow;

 // If status moves away from Completed, clear it
 if (existing.Status != RequestStatus.Completed)
     existing.CompletedAt = null;

 var updated = await _repo.UpdateAsync(existing);
 return updated == null ? (false, "Update failed.", null) : (true, "", ToDto(updated));
 }

     public async Task<bool> DeleteAsync(int id)
        => await _repo.DeleteAsync(id);

    // ── Private helper ──────────────────────────────
    private static ServiceRequestDto ToDto(ServiceRequest e)
        => new ServiceRequestDto(
            e.Id,
            e.Title,
            e.Description,
            (int)e.Status,
            e.CreatedAt,
            e.CompletedAt
        );
}

