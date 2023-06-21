using Project.Services.Models;

namespace Project.Services.Services
{
    public interface IModelService
    {
        Task<List<VehicleModel>> GetAllVehicleModels();
        Task<VehicleModel?> GetVehicleModelById(int? id);
        Task<VehicleModel> GetModelDetails(int? id);
        Task CreateVehicleModel(VehicleModel vehicleModel);
        Task UpdateVehicleModel(VehicleModel vehicleModel);
        Task DeleteVehicleModel(int id);
    }
}
