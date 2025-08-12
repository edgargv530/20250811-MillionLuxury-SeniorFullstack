using AutoMapper;
using MillionLuxury.TechhicalTest.Application.Models.Owners;
using MillionLuxury.TechhicalTest.Domain.Entitites;

namespace MillionLuxury.TechhicalTest.Application.Profiles
{
    internal class OwnerModelProfile : Profile
    {
        public OwnerModelProfile()
        {
            CreateMap<OwnerModel, Owner>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (src.Id.Equals(null) || src.Id.Equals(Guid.Empty)) ? Guid.NewGuid() : src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Name) ? string.Empty : src.Name.Trim()))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Address) ? string.Empty : src.Address.Trim()))
                //.ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Photo))
                //.ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
                .ReverseMap();
        }
    }
}
