using Project.Services.Models;
using Project.Services.Utilities;
using X.PagedList;

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

        //UTILITIES
        Task<List<VehicleMake>> SortMakesAsync(List<VehicleMake> makes, SortingParameters sortingParams);
        Task<List<VehicleMake>> FilterMakesAsync(List<VehicleMake> makes, FilteringParameters filteringParams);
        Task<IPagedList<VehicleMake>> PageMakesAsync(List<VehicleMake> models, PagingParameters pagingParameters);
    }
}
