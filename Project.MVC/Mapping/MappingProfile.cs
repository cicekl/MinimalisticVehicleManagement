using AutoMapper;
using Project.MVC.Models;
using Project.Services.Models;
using X.PagedList;

namespace Project.MVC.Mapping;

public class MappingProfile: Profile
{
    public MappingProfile() {
        CreateMap<VehicleMake, VehicleMakeViewModel>().ReverseMap();
        CreateMap<VehicleModel, VehicleModelViewModel>().ReverseMap();
        CreateMap<IPagedList<VehicleMake>, IPagedList<VehicleMakeViewModel>>()
               .ConvertUsing((source, destination, context) =>
               {
                   var mappedList = context.Mapper.Map<IEnumerable<VehicleMakeViewModel>>(source);
                   return new StaticPagedList<VehicleMakeViewModel>(mappedList, source.GetMetaData());
               });
        CreateMap<IPagedList<VehicleModel>, IPagedList<VehicleModelViewModel>>()
               .ConvertUsing((source, destination, context) =>
               {
                   var mappedList = context.Mapper.Map<IEnumerable<VehicleModelViewModel>>(source);
                   return new StaticPagedList<VehicleModelViewModel>(mappedList, source.GetMetaData());
               });
    }
}
