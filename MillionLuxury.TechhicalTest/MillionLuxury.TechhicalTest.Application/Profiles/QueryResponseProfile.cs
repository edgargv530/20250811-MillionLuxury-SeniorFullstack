using AutoMapper;
using MillionLuxury.TechhicalTest.Domain.Values.QueryOptions;

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
