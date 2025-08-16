using MillionLuxury.TechhicalTest.Domain.Entitites;
using MillionLuxury.TechhicalTest.Domain.ValuesObjects.QueryOptions;

namespace MillionLuxury.TechhicalTest.Domain.Repositories
{
    public interface IOwnerRepository
    {
        Task<Owner> GetById(Guid id);

        Task<QueryResponse<Owner>> GetData(QueryRequest queryRequest);

        Task<Owner> Add(Owner owner);

        Task<Owner> Update(Owner owner);

        Task Delete(Guid id);
    }
}
