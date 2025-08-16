using MillionLuxury.TechhicalTest.Application.Models.Owners;
using MillionLuxury.TechhicalTest.Domain.ValuesObjects.QueryOptions;

namespace MillionLuxury.TechhicalTest.Application.UseCases.Owners
{
    public interface IOwnersUseCase
    {
        Task<OwnerModel> Add(OwnerModel ownerModel);

        Task<QueryResponse<OwnerModel>> GetData(QueryRequest queryRequest);

        Task<OwnerModel> GetById(Guid id);

        Task<OwnerModel> Update(OwnerModel ownerModel);

        Task Delete(Guid id);
    }
}
