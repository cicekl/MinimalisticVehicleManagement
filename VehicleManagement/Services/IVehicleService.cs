using Project.Service.Models;
namespace Project.Service.Services
{
    public interface IVehicleService
    {

        //VEHICLE MAKE
         Task<List<VehicleMake>> GetAllVehicleMakes(); 
        Task<VehicleMake> GetVehicleMakeById(int? id);
        Task<VehicleMake> GetMakeDetails(int? id);
        Task CreateVehicleMake(VehicleMake vehicleMake);
        Task UpdateVehicleMake(VehicleMake vehicleMake);
        Task DeleteVehicleMake(int id);

        //VEHICLE MODEL
        Task<List<VehicleModel>> GetAllVehicleModels();
        Task<VehicleModel?> GetVehicleModelById(int? id);
        Task<VehicleModel> GetModelDetails(int? id);
        Task CreateVehicleModel(VehicleModel vehicleModel);
        Task UpdateVehicleModel(VehicleModel vehicleModel);
        Task DeleteVehicleModel(int id);

        }
}
