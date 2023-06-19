using Microsoft.EntityFrameworkCore;
using Project.Service.DataAccess;
using Project.Service.Models;

namespace Project.Service.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly VehicleDBContext _dbContext;


        public VehicleService(VehicleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        //VEHICLE MAKES METHODS

        public async Task<List<VehicleMake>> GetAllVehicleMakes()
          {
            return await _dbContext.VehicleMake.ToListAsync();
        } 

        public async Task<VehicleMake> GetVehicleMakeById(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return await _dbContext.VehicleMake.FindAsync(id);
        }

        public async Task CreateVehicleMake(VehicleMake vehicleMake)
        {
            _dbContext.Add(vehicleMake);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateVehicleMake(VehicleMake vehicleMake)
        {
            _dbContext.Update(vehicleMake);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<VehicleMake> GetMakeDetails(int? id)
        {
            return await _dbContext.VehicleMake
                .Include(make => make.Models)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task DeleteVehicleMake(int id)
        {
            var vehicleMake = await _dbContext.VehicleMake.FindAsync(id);
            if (vehicleMake != null)
            {
                _dbContext.VehicleMake.Remove(vehicleMake);
                await _dbContext.SaveChangesAsync();
            }
        }

        //VEHICLE MODEL METHODS

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
