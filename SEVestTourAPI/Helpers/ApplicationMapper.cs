using AutoMapper;
using SEVestTourAPI.Models;
using SEVestTourAPI.Entities;


namespace SEVestTourAPI.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, LoginModel>().ReverseMap();
            CreateMap<User, RegisterModel>().ReverseMap();
            CreateMap<Role, RoleModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<Measurement, MeasurementModel>().ReverseMap();
            CreateMap<Voucher, VoucherModel>().ReverseMap();
            CreateMap<Style, StyleModel>().ReverseMap();
            CreateMap<StyleOption, StyleOptionModel>().ReverseMap();
            CreateMap<ShipperPartner, ShipperPartnerModel>().ReverseMap();
            CreateMap<Store, StoreModel>().ReverseMap();
            CreateMap<Booking , BookingModel>().ReverseMap();
            CreateMap<Order, OrderModel>().ReverseMap();
            CreateMap<Fabric, FabricModel>().ReverseMap();
            CreateMap<Lining, LiningModel>().ReverseMap();
            CreateMap<BankingAccount, BankingAccountModel>().ReverseMap();
        }
    }
}
