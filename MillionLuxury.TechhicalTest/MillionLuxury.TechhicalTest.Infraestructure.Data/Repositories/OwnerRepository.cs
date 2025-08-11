using AutoMapper;
using MillionLuxury.TechhicalTest.Domain.Entitites;
using MillionLuxury.TechhicalTest.Domain.Repositories;
using MillionLuxury.TechhicalTest.Infraestructure.Data.DataModels;
using MongoDB.Driver;

namespace MillionLuxury.TechhicalTest.Infraestructure.Data.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        protected readonly string connectionString = "mongodb://localhost:27017";
        protected readonly string databaseName = "PropertiesDB";
        protected readonly string collectionName = "Owners";

        private readonly IMapper _mapper;
        private IMongoCollection<OwnerDataModel> _collection;

        public OwnerRepository(IMapper mapper)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            _collection = db.GetCollection<OwnerDataModel>(collectionName);
            _mapper = mapper;
        }

        public async Task Add(Owner owner)
        {
            owner.Validate();

            var ownerDataModel = _mapper.Map<OwnerDataModel>(owner);
            await _collection.InsertOneAsync(ownerDataModel);
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Owner> GetById(Guid id)
        {
            var ownerDataModel = await _collection.FindAsync(o => o.Id.Equals(id));
            var owner = _mapper.Map<Owner>(ownerDataModel);
            return owner;
        }

        public async Task<IEnumerable<Owner>> GetData()
        {
            var ownerDataModels = await _collection.Find(_ => true).ToListAsync();
            var owners = _mapper.Map<IEnumerable<Owner>>(ownerDataModels);
            return owners;
        }

        public Task Update(Owner owner)
        {
            throw new NotImplementedException();
        }
    }
}
