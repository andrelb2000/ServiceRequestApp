using ServiceRequestApp.Domain.Entities;
namespace ServiceRequestApp.Application.Interfaces;
public interface IServiceRequestRepository
{
    Task<List<ServiceRequest>>  GetAllAsync();         // R.etrieve (R) from C.R.U.D
    Task<ServiceRequest?>       GetByIdAsync(int id);  // GUID R.etrieve (R) from C.R.U.D 
    Task<ServiceRequest>        AddAsync(ServiceRequest request); // C.reate (C).R.U.D 
    Task<ServiceRequest?>       UpdateAsync(ServiceRequest request); //U.pdate(U) C.R.U.D 
    Task<bool>                  DeleteAsync(int id);                 //D.elete(D) C.R.U.D 
}