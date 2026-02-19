namespace ServiceRequestApp.Application.DTOs;
public record UpdateServiceRequestDto(
    string Title,
    string Description,
    int Status
);