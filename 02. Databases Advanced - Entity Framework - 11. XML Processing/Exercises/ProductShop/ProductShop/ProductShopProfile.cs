using AutoMapper;
using ProductShop.Dtos.Export;
using ProductShop.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            this.CreateMap<Product, ProductsInRangeDto>()
                .ForMember(x=>x.Buyer,y=>y.MapFrom(z=>$"{z.Buyer.FirstName} {z.Buyer.LastName}"));

            this.CreateMap<User, SoldItemsDto>();
            this.CreateMap<Product, SoldProductDto>();


            CreateMap<ICollection<UserDto>, UsersAndProductsDto>()
                .ForMember(x => x.Users, y => y.MapFrom(obj => obj.Take(10)))
                .ForMember(x => x.Count, y => y.MapFrom(obj => obj.Count));
            CreateMap<User, UserDto>()
                .ForMember(x => x.SoldProducts, y => y.MapFrom(obj => obj.ProductsSold));

            CreateMap<User, SoldProductsWithCountDto>()
                .ForMember(x => x.Products, y => y.MapFrom(obj => obj.ProductsSold));
            CreateMap<ICollection<Product>, SoldProductsWithCountDto>()
                .ForMember(x => x.Products, y => y.MapFrom(obj => obj.OrderByDescending(z => z.Price)))
                .ForMember(x => x.Count, y => y.MapFrom(obj => obj.Count));
        }
    }
}
