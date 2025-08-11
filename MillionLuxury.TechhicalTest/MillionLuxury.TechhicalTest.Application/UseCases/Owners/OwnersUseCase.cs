using AutoMapper;
using MillionLuxury.TechhicalTest.Application.Models.Owners;
using MillionLuxury.TechhicalTest.Domain.Entitites;
using MillionLuxury.TechhicalTest.Domain.Repositories;

namespace MillionLuxury.TechhicalTest.Application.UseCases.Owners
{
    public class OwnersUseCase : IOwnersUseCase
    {
        private readonly IMapper _mapper;
        private readonly IOwnerRepository _ownerRepository;

        public OwnersUseCase(
            IMapper mapper,
            IOwnerRepository ownerRepository)
        {
            _mapper = mapper;
            _ownerRepository = ownerRepository;
        }

        public async Task<OwnerModel> Add(OwnerModel ownerModel)
        {
            var owner = _mapper.Map<Owner>(ownerModel);
            await _ownerRepository.Add(owner);
            var ownerAdded = _ownerRepository.GetById(owner.Id);
            var ownerModelAdded = _mapper.Map<OwnerModel>(ownerAdded);
            return ownerModelAdded;
        }
    }
}
