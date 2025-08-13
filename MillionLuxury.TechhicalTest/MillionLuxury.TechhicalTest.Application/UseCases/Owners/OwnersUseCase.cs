using AutoMapper;
using MillionLuxury.TechhicalTest.Application.Models.Owners;
using MillionLuxury.TechhicalTest.Domain.Entitites;
using MillionLuxury.TechhicalTest.Domain.Repositories;
using MillionLuxury.TechhicalTest.Domain.Values.QueryOptions;

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
            var ownerAdded = await _ownerRepository.Add(owner);
            var ownerModelAdded = _mapper.Map<OwnerModel>(ownerAdded);
            return ownerModelAdded;
        }

        public async Task Delete(Guid id)
        {
            await _ownerRepository.Delete(id);
        }

        public async Task<QueryResponse<OwnerModel>> GetData(QueryRequest queryRequest)
        {
            var owners = await _ownerRepository.GetData(queryRequest);
            var queryResponse = _mapper.Map<QueryResponse<OwnerModel>>(owners);
            return queryResponse;
        }

        public async Task<OwnerModel> GetById(Guid id)
        {
            var owner = await _ownerRepository.GetById(id);
            var ownerModel = _mapper.Map<OwnerModel>(owner);
            return ownerModel;
        }

        public async Task<OwnerModel> Update(OwnerModel ownerModel)
        {
            var owner = _mapper.Map<Owner>(ownerModel);
            var ownerAdded = await _ownerRepository.Update(owner);
            var ownerModelAdded = _mapper.Map<OwnerModel>(ownerAdded);
            return ownerModelAdded;
        }
    }
}
