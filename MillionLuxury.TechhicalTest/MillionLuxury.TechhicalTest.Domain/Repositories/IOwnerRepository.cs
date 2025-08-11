using MillionLuxury.TechhicalTest.Domain.Entitites;

namespace MillionLuxury.TechhicalTest.Domain.Repositories
{
    public interface IOwnerRepository
    {
        Task<Owner> GetById(Guid id);

        Task<IEnumerable<Owner>> GetData();

        Task Add(Owner owner);

        Task Update(Owner owner);

        Task Delete(Guid id);
    }
}
