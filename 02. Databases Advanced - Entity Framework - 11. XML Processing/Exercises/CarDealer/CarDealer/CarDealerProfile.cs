using AutoMapper;
using CarDealer.Dtos.Import;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            //this.CreateMap<Car, ImportCarDto>()
            //    .ForMember(x => x.Parts, y => y.MapFrom(z => z.PartCars));
            //this.CreateMap<Part, ImportPartIdDto>();
                //.ForMember(x => x.Id, y => y.MapFrom(z => z.PartCars));
        }
    }
}
