using ServiceRequestApp.Domain.Enums;
namespace ServiceRequestApp.Domain.Entities;
public class ServiceRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public RequestStatus Status { get; set; }
    // Example of DateTimeOffset properties for tracking creation and completion times
    // 2024-01-15T14:30:00-05:00
    public DateTimeOffset?  CreatedAt { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }
}
