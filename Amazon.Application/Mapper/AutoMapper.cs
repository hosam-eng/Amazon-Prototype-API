using Amazon.Domain;
using Amazon.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Application.Mapper
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<Category, SubCategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category,arCategoryDTO>().ReverseMap();
            CreateMap<Category, arsubcategory>().ReverseMap();
            CreateMap<Product, ShowProductDTO>().ReverseMap();
            CreateMap<Product, ArShowproduct>().ReverseMap();
            CreateMap<Product,AddUpdateProductDTO>().ReverseMap();
            CreateMap<ApplicationUser,UserRegisterDTO>().ReverseMap();
            CreateMap<ApplicationUser,UserLoginDTO>().ReverseMap();
            CreateMap<ApplicationUser, UserProfileDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemShow>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Image, ImageDTO>().ReverseMap();
            CreateMap<Rating, RatingDTO>().ReverseMap();
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<City, CitiesListDTO>().ReverseMap();
            CreateMap<shippingAddress, AddAndEditShippingAddressDTO>().ReverseMap();
        }
    }
}
