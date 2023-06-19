using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Services.DataAccess;
using Project.Services.Models;
using Project.Services.Utilities;
using X.PagedList;

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

        public Task<List<VehicleMake>> SortMakesAsync(List<VehicleMake> makes, SortingParameters sortingParams)
        {
            switch (sortingParams.sortOrder)
            {
                case "name_desc":
                    return Task.FromResult(makes.OrderByDescending(make => make.Name).ToList());
                default:
                    return Task.FromResult(makes.OrderBy(make => make.Name).ToList());
            }
        }

        public Task<List<VehicleMake>> FilterMakesAsync(List<VehicleMake> makes, FilteringParameters filteringParams)
        {
            if (!String.IsNullOrEmpty(filteringParams.searchString))
            {
                return Task.FromResult(makes.Where(s => s.Name.IndexOf(filteringParams.searchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList());
            }
            else
            {
                return Task.FromResult(makes);
            }
        }

        public Task<IPagedList<VehicleMake>> PageMakesAsync(List<VehicleMake> makes, PagingParameters pagingParameters)
        {
            pagingParameters.pageSize = 5;
            var pagedVehicleMakes = makes.ToPagedList(pagingParameters.pageNumber.GetValueOrDefault(1), pagingParameters.pageSize);
            return Task.FromResult(pagedVehicleMakes);
        }


    }
}

