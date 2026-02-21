using System.Net.Http.Json;
using ServiceRequestApp.Blazor.Models;

namespace ServiceRequestApp.Blazor.Services;

public class ServiceRequestApi {
 private readonly HttpClient _http;

 public ServiceRequestApi(HttpClient http)  {
 _http = http;
 }
 public async Task<List<ServiceRequestDto>?> GetAllAsync()  {
 return await _http.GetFromJsonAsync<List<ServiceRequestDto>>(
 "api/servicerequests");
 }
 public async Task CreateAsync(ServiceRequestDto dto)  {
 await _http.PostAsJsonAsync("api/servicerequests", dto);
 }
}
