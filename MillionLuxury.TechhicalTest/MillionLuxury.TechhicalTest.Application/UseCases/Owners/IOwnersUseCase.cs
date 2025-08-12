using MillionLuxury.TechhicalTest.Application.Models.Owners;

namespace MillionLuxury.TechhicalTest.Application.UseCases.Owners
{
    public interface IOwnersUseCase
    {
        Task<OwnerModel> Add(OwnerModel ownerModel);

        Task<IEnumerable<OwnerModel>> GetData();

        Task<OwnerModel> GetById(Guid id);

        Task<OwnerModel> Update(OwnerModel ownerModel);

        Task Delete(Guid id);
    }
}
