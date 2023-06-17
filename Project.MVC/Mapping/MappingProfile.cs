using AutoMapper;
using Project.Service.Models;
namespace Project.MVC.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {
            CreateMap<VehicleMake, VehicleMakeViewModel>().ReverseMap();
            CreateMap<VehicleModel, VehicleModelViewModel>().ReverseMap();
        }
    }
}
