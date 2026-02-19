using ServiceRequestApp.Application.DTOs;
namespace ServiceRequestApp.Application.Interfaces;

public interface IServiceRequestService
{
    Task<List<ServiceRequestDto>> GetAllAsync();
    Task<ServiceRequestDto?> GetByIdAsync(int id);
    Task<(bool ok, string error,
          ServiceRequestDto? created)>
        CreateAsync(CreateServiceRequestDto dto);
    Task<(bool ok, string error,
          ServiceRequestDto? updated)>
        UpdateAsync(int id, UpdateServiceRequestDto dto);
    Task<bool> DeleteAsync(int id);
}

