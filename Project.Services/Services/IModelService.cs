using Project.Services.Models;
using Project.Services.Utilities;
using X.PagedList;

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

        //UTILITIES
        Task<List<VehicleModel>> SortModelsAsync(List<VehicleModel> models, SortingParameters sortingParams);
        Task<List<VehicleModel>> FilterModelsAsync(List<VehicleModel> models, FilteringParameters filteringParams);
        Task<IPagedList<VehicleModel>> PageModelsAsync(List<VehicleModel> models, PagingParameters pagingParameters);
    }
}
