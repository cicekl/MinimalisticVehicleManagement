using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Services.DataAccess;
using Project.Services.Models;

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
    }
}
