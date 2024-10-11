using AutoMapper;
using VestTour.Models;

namespace VestTour.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper() 
        {
            CreateMap<Fabric, FabricModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<Lining, LiningModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();

        }
    }
}
