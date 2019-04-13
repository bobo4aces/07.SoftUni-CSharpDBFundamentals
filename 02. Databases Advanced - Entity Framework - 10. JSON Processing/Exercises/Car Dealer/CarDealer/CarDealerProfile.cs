using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using CarDealer.DTO;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<Part, PartDto>()
                .ForMember(x => x.SupplierId, y => y.MapFrom(z => z.SupplierId));
            this.CreateMap<Customer, CustomerDto>()
                .ForMember(x=>x.BirthDate,y=>y.MapFrom(z=>z.BirthDate.ToString("dd/MM/yyyy")));

            this.CreateMap<CarInsertDto, Car>();

            this.CreateMap<Supplier, SupplierDto>()
                .ForMember(x => x.PartsCount, y => y.MapFrom(z => z.Parts.Count));
            
        }
    }
}
