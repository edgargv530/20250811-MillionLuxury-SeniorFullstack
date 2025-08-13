using AutoMapper;
using MillionLuxury.TechhicalTest.Domain.Entitites;
using MillionLuxury.TechhicalTest.Domain.Enums;
using MillionLuxury.TechhicalTest.Domain.Repositories;
using MillionLuxury.TechhicalTest.Domain.Values.QueryOptions;
using MillionLuxury.TechhicalTest.Infraestructure.Data.DataModels;
using MillionLuxury.TechhicalTest.Infraestructure.Data.DbConnection;
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

        public async Task<QueryResponse<Owner>> GetData(QueryRequest queryRequest)
        {
            ////(contains(Name, 'nombre') or endswith(Name, '7')) and address eq 'Address 6'
            //var builderFilter = Builders<OwnerDataModel>.Filter;
            ////builderFilter.Where(o => o.Name.Contains("Name") || o.Name.EndsWith("7"));
            //var filter = builderFilter.Where(o => (o.Name.ToUpper().Contains("nombre".ToUpper()) || o.Name.EndsWith("7")) || o.Address.Equals("Address 6"));

            var filter = queryRequest.Filters != null && queryRequest.Filters.Any()
                    ? BuildMongoFilter(queryRequest.Filters, null)
                    : Builders<OwnerDataModel>.Filter.Empty;

            var sortDefinitions = new List<SortDefinition<OwnerDataModel>>();

            if (queryRequest.OrdersBy is not null && queryRequest.OrdersBy.Any())
            {
                var sortFieldMap = new Dictionary<string, Func<bool, SortDefinition<OwnerDataModel>>>(StringComparer.OrdinalIgnoreCase)
                {
                    { nameof(Owner.Name), ascending => ascending ? Builders<OwnerDataModel>.Sort.Ascending(o => o.Name) : Builders<OwnerDataModel>.Sort.Descending(o => o.Name) },
                    { nameof(Owner.Address), ascending => ascending ? Builders<OwnerDataModel>.Sort.Ascending(o => o.Address) : Builders<OwnerDataModel>.Sort.Descending(o => o.Address) }
                };

                foreach (var orderItem in queryRequest.OrdersBy)
                {
                    if (sortFieldMap.TryGetValue(orderItem.FieldName, out var sortFunc))
                    {
                        sortDefinitions.Add(sortFunc(orderItem.Ascending));
                    }
                }
            }


            var ownerDataModels = await _collection
                .Find(filter)
                .Limit(queryRequest.Top)
                .Skip(queryRequest.Skip)
                .Sort(Builders<OwnerDataModel>.Sort.Combine(sortDefinitions))
                .ToListAsync();

            var owners = _mapper.Map<IEnumerable<Owner>>(ownerDataModels);
            var queryResponse = new QueryResponse<Owner>
            {
                Top = queryRequest.Top ?? 10,
                Skip = queryRequest.Skip ?? 0,
                TotalRows = await _collection.CountDocumentsAsync(filter),
                Data = owners
            };
            return queryResponse;
        }

        private FilterDefinition<OwnerDataModel> BuildMongoFilter(IEnumerable<IQueryFilter> filters, QueryFilterComplex parent)
        {
            var builder = Builders<OwnerDataModel>.Filter;
            var filterList = new List<FilterDefinition<OwnerDataModel>>();

            foreach (var filter in filters)
            {
                if (filter is QueryFilterSentence sentence)
                {
                    // Aquí puedes expandir según los operadores que soportes
                    switch (sentence.Operator)
                    {
                        case FilterOperator.Equal:
                            filterList.Add(builder.Eq(sentence.FieldName, sentence.Value));
                            break;
                        case FilterOperator.Contains:
                            filterList.Add(builder.Regex(sentence.FieldName, new MongoDB.Bson.BsonRegularExpression(sentence.Value!.ToString(), "i")));
                            break;
                        case FilterOperator.StartsWith:
                            filterList.Add(builder.Regex(sentence.FieldName, new MongoDB.Bson.BsonRegularExpression("^" + sentence.Value!.ToString(), "i")));
                            break;
                        case FilterOperator.EndsWith:
                            filterList.Add(builder.Regex(sentence.FieldName, new MongoDB.Bson.BsonRegularExpression(sentence.Value!.ToString() + "$", "i")));
                            break;
                            // Agrega más operadores según sea necesario
                    }
                }
                else if (filter is QueryFilterComplex complex)
                {
                    var innerFilter = BuildMongoFilter(complex.Filters, complex);
                    filterList.Add(innerFilter);
                }
            }

            //return filterList;

            // Combina los filtros según el predicado (AND/OR)
            if (parent is QueryFilterComplex complexFilter)
            {
                return complexFilter.PredicateType == FilterPredicateType.And
                    ? builder.And(filterList)
                    : builder.Or(filterList);
            }

            // Por defecto, combina con AND
            //return builder.And(filterList);
            return null!;
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
