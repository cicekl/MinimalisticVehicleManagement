using AutoMapper;
using Project.MVC.Models;
using Project.Service.Models;

namespace Project.MVC.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {
            CreateMap<VehicleMake, VehicleMakeViewModel>();
            CreateMap<VehicleModel, VehicleModelViewModel>();
        }
    }
}
