using Project.Services.Models;

namespace Project.Services.Services
{
    public interface IMakeService
    {
        Task<List<VehicleMake>> GetAllVehicleMakes();
        Task<VehicleMake> GetVehicleMakeById(int? id);
        Task<VehicleMake> GetMakeDetails(int? id);
        Task CreateVehicleMake(VehicleMake vehicleMake);
        Task UpdateVehicleMake(VehicleMake vehicleMake);
        Task DeleteVehicleMake(int id);
    }
}
