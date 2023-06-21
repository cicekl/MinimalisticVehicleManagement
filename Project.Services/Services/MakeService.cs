using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Services.DataAccess;
using Project.Services.Models;

namespace Project.Services.Services
{
    public class MakeService : IMakeService
    {

        private readonly VehicleDBContext _dbContext;

        public MakeService(VehicleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<VehicleMake>> GetAllVehicleMakes()
        {
            return await _dbContext.VehicleMake.ToListAsync();
        }

        public async Task CreateVehicleMake(VehicleMake vehicleMake)
        {
            _dbContext.Add(vehicleMake);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<VehicleMake> GetVehicleMakeById(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return await _dbContext.VehicleMake.FindAsync(id);
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


    }
}

