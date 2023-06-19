using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Services.DataAccess;
using Project.Services.Models;
using Project.Services.Utilities;
using X.PagedList;

namespace Project.Services.Services
{
    public class ModelService : IModelService
    {

        private readonly VehicleDBContext _dbContext;

        public ModelService(VehicleDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<VehicleModel>> GetAllVehicleModels()
        {
            return await _dbContext.VehicleModel
                .Include(vm => vm.Make)
                .ToListAsync();
        }


        public async Task<VehicleModel?> GetVehicleModelById(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return await _dbContext.VehicleModel.FindAsync(id);
        }

        public async Task CreateVehicleModel(VehicleModel vehicleModel)
        {
            _dbContext.Add(vehicleModel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateVehicleModel(VehicleModel vehicleModel)
        {
            _dbContext.Update(vehicleModel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<VehicleModel> GetModelDetails(int? id)
        {
            return await _dbContext.VehicleModel
        .Include(v => v.Make)
        .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task DeleteVehicleModel(int id)
        {
            var vehicleModel = await _dbContext.VehicleModel.FindAsync(id);
            if (vehicleModel != null)
            {
                _dbContext.VehicleModel.Remove(vehicleModel);
                await _dbContext.SaveChangesAsync();
            }
        }

        public Task<List<VehicleModel>> SortModelsAsync(List<VehicleModel> models, SortingParameters sortingParams)
        {
            switch (sortingParams.sortOrder)
            {
                case "name_desc":
                    return Task.FromResult(models.OrderByDescending(model => model.Name).ToList());
                default:
                    return Task.FromResult(models.OrderBy(model => model.Name).ToList());
            }
        }

        public Task<List<VehicleModel>> FilterModelsAsync(List<VehicleModel> models, FilteringParameters filteringParams)
        {
            if (!String.IsNullOrEmpty(filteringParams.searchString))
            {
                return Task.FromResult(models.Where(s => s.Make.Name.IndexOf(filteringParams.searchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList());
            }
            else
            {
                return Task.FromResult(models.ToList());
            }
        }

        public Task<IPagedList<VehicleModel>> PageModelsAsync(List<VehicleModel> models, PagingParameters pagingParameters)
        {
            pagingParameters.pageSize = 5;
            var pagedVehicleModels = models.ToPagedList(pagingParameters.pageNumber.GetValueOrDefault(1), pagingParameters.pageSize);
            return Task.FromResult(pagedVehicleModels);
        }
    }
}
