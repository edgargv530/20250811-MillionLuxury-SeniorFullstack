using MillionLuxury.TechhicalTest.Application.Models.Owners;

namespace MillionLuxury.TechhicalTest.Application.UseCases.Owners
{
    public interface IOwnersUseCase
    {
        Task<OwnerModel> Add(OwnerModel ownerModel);
    }
}
