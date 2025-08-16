using AutoMapper;
using MillionLuxury.TechhicalTest.Domain.ValuesObjects.QueryOptions;

namespace MillionLuxury.TechhicalTest.Application.Profiles
{
    internal class QueryResponseProfile : Profile
    {
        public QueryResponseProfile()
        {
            CreateMap(typeof(QueryResponse<>), typeof(QueryResponse<>));
        }
    }
}
