using AutoMapper;
using MillionLuxury.TechhicalTest.Domain.Entitites;
using MillionLuxury.TechhicalTest.Domain.Repositories;
using MillionLuxury.TechhicalTest.Infraestructure.Data.DataModels;
using MillionLuxury.TechhicalTest.Infraestructure.Data.Factories;
using MillionLuxury.TechhicalTest.Resources.Owners;
using MongoDB.Driver;

namespace MillionLuxury.TechhicalTest.Infraestructure.Data.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly IMapper _mapper;
        private readonly IMongoDbConnectionProperties _mongoDbConnectionProperties;
        private readonly IMongoCollection<OwnerDataModel> _collection;

        public OwnerRepository(
            IMapper mapper,
            IMongoDbConnectionProperties mongoDbConnectionProperties)
        {
            _mongoDbConnectionProperties = mongoDbConnectionProperties;
            var client = new MongoClient(_mongoDbConnectionProperties.ConnectionString);
            var db = client.GetDatabase(_mongoDbConnectionProperties.DatabaseName);
            _collection = db.GetCollection<OwnerDataModel>(_mongoDbConnectionProperties.CollectionOwnerName);
            _mapper = mapper;
        }

        public async Task<Owner> Add(Owner owner)
        {
            owner.Validate();
            var ownerDataModel = _mapper.Map<OwnerDataModel>(owner);
            await _collection.InsertOneAsync(ownerDataModel);
            var ownerAdded = _mapper.Map<Owner>(ownerDataModel);
            return ownerAdded;
        }

        public async Task Delete(Guid id)
        {
            var filter = Builders<OwnerDataModel>.Filter.Eq(o => o.Id, id);
            var result = await _collection.DeleteOneAsync(filter);
            if (!result.IsAcknowledged || result.DeletedCount == 0)
            {
                throw new ApplicationException(OwnersResources.ErrorDeleting);
            }
        }

        public async Task<Owner> GetById(Guid id)
        {
            var filter = Builders<OwnerDataModel>.Filter.Eq(o => o.Id, id);
            var ownerDataModel = await _collection.Find(filter).FirstOrDefaultAsync();
            var owner = _mapper.Map<Owner>(ownerDataModel);
            return owner;
        }

        public async Task<IEnumerable<Owner>> GetData()
        {
            var ownerDataModels = await _collection.Find(_ => true).ToListAsync();
            var owners = _mapper.Map<IEnumerable<Owner>>(ownerDataModels);
            return owners;
        }

        public async Task<Owner> Update(Owner owner)
        {
            owner.Validate();
            var ownerDataModel = _mapper.Map<OwnerDataModel>(owner);
            var filter = Builders<OwnerDataModel>.Filter.Eq(o => o.Id, owner.Id);
            var result = await _collection.ReplaceOneAsync(filter, ownerDataModel);
            if (!result.IsAcknowledged || result.ModifiedCount == 0)
            {
                throw new ApplicationException(OwnersResources.ErrorUpdating);
            }
            var ownerAdded = _mapper.Map<Owner>(ownerDataModel);
            return ownerAdded;
        }
    }
}
