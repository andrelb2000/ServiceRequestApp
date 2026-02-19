using Microsoft.EntityFrameworkCore;
using ServiceRequestApp.Application.Interfaces;
using ServiceRequestApp.Domain.Entities;
using ServiceRequestApp.Infrastructure.Data;

namespace ServiceRequestApp.Infrastructure.Repositories;

public class ServiceRequestRepository : IServiceRequestRepository{
             private readonly AppDbContext _db;
             public  ServiceRequestRepository(AppDbContext db) => _db = db;
             public async Task<List<ServiceRequest>> GetAllAsync() 
                    => await _db.ServiceRequests
                             .OrderByDescending(r => r.CreatedAt)
                             .ToListAsync();
             public async Task<ServiceRequest?> GetByIdAsync(int id)
                    => await _db.ServiceRequests.FindAsync(id);
           
           public async Task<ServiceRequest> AddAsync(ServiceRequest request){
                 _db.ServiceRequests.Add(request);
                 await _db.SaveChangesAsync();
                 return request;
           }
//////////////// Rest of the code in the next page ////////////////////////
///--- Rest of the code

 public async Task<ServiceRequest?> UpdateAsync(
    ServiceRequest request) {
      var existing = await _db.ServiceRequests
                    .FindAsync(request.Id);
      if (existing == null) return null;
      existing.Title = request.Title;
      existing.Description = request.Description;
      existing.Status = request.Status;
      existing.CompletedAt = request.CompletedAt;
      await _db.SaveChangesAsync();
      return existing;
   }

   public async Task<bool> DeleteAsync(int id){
         var existing = await _db.ServiceRequests
             .FindAsync(id);
         if (existing == null) return false;
         _db.ServiceRequests.Remove(existing);
         await _db.SaveChangesAsync();
         return true;
   }
}