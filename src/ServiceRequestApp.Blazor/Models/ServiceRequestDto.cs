namespace ServiceRequestApp.Blazor.Models;

public class ServiceRequestDto
{
 public int Id { get; set; }
 public string Title { get; set; } = "";
 public string Description { get; set; } = "";
 public int Status { get; set; }
}
