using AutoMapper;
using MillionLuxury.TechhicalTest.Domain.Entitites;
using MillionLuxury.TechhicalTest.Infraestructure.Data.DataModels;

namespace MillionLuxury.TechhicalTest.Infraestructure.Data.Profiles
{
    internal class OwnerDataModelProfile : Profile
    {
        public OwnerDataModelProfile()
        {
            CreateMap<Owner, OwnerDataModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ReverseMap();
        }
    }
}
