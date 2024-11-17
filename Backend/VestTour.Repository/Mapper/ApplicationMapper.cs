using AutoMapper;
using VestTour.Repository.Models;
using VestTour.Domain.Entities;
using VestTour.Repository.Models;
using VestTour.Repository.Models;

namespace VestTour.Repository.Mapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, UpdateUserModel>().ReverseMap();
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
            CreateMap<Booking, BookingModel>().ReverseMap();
            CreateMap<Payment, PaymentModel>().ReverseMap();
            CreateMap<Product, ProductDetailsModel>().ReverseMap();
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<TailorPartner, TailorPartnerModel>().ReverseMap();
            CreateMap<ProcessingTailor, ProcessingTailorModel>().ReverseMap();
            CreateMap<ProductInStore, ProductInStoreModel>().ReverseMap();


        }
    }
}
