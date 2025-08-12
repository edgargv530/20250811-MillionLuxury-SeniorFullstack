using MillionLuxury.TechhicalTest.Domain.Entitites;

namespace MillionLuxury.TechhicalTest.Domain.Repositories
{
    public interface IOwnerRepository
    {
        Task<Owner> GetById(Guid id);

        Task<IEnumerable<Owner>> GetData();

        Task<Owner> Add(Owner owner);

        Task<Owner> Update(Owner owner);

        Task Delete(Guid id);
    }
}
