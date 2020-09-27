using AutoMapper;
using SmartAppData.DAL.Enums;
using SmartAppData.Extensions;

namespace SmartAppData.RestApi.Models.Product
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<DAL.Entities.Product, ProductResource>()
                .ForMember(src => src.UnitOfMeasurement,
                           opt => opt.MapFrom(src => src.UnitOfMeasurement.ToDescriptionString()));

            CreateMap<SaveProductResource, DAL.Entities.Product>()
                .ForMember(src => src.UnitOfMeasurement, opt => opt.MapFrom(src => (EUnitOfMeasurement)src.UnitOfMeasurement));
        }
    }
}
