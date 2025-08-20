using AutoMapper;
using MillionLuxury.TechhicalTest.Domain.Entitites;
using MillionLuxury.TechhicalTest.Domain.Enums;
using MillionLuxury.TechhicalTest.Domain.Repositories;
using MillionLuxury.TechhicalTest.Domain.ValuesObjects.QueryOptions;
using MillionLuxury.TechhicalTest.Infraestructure.Data.DataModels;
using MillionLuxury.TechhicalTest.Infraestructure.Data.DbConnection;
using MillionLuxury.TechhicalTest.Resources.Owners;
using MongoDB.Bson;
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
            var filter = BuildMongoFilter(queryRequest.Filters, null!);
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
                Top = queryRequest.Top.HasValue ? queryRequest.Top.Value : 100,
                Skip = queryRequest.Skip.HasValue ? queryRequest.Skip.Value : 0,
                TotalRows = await _collection.CountDocumentsAsync(filter),
                Data = owners
            };
            return queryResponse;
        }

        private FilterDefinition<OwnerDataModel> BuildMongoFilter(IEnumerable<IQueryFilter> filters, QueryFilterComplex parent)
        {
            if (filters is null || !filters.Any())
            {
                return Builders<OwnerDataModel>.Filter.Empty;
            }

            var filterList = new List<FilterDefinition<OwnerDataModel>>();
            foreach (var queryFilter in filters)
            {
                if (queryFilter is QueryFilterComplex queryFilterComplex)
                {
                    filterList.Add(BuildMongoFilter(queryFilterComplex.Filters, queryFilterComplex));
                }
                else if (queryFilter is QueryFilterSentence queryFilterSentence)
                {
                    filterList.Add(BuildMongoFilterSentence(queryFilterSentence));
                }
            }

            if (parent is not null && parent.PredicateType == FilterPredicateType.Or)
            {
                return Builders<OwnerDataModel>.Filter.Or(filterList);
            }

            return Builders<OwnerDataModel>.Filter.And(filterList);
        }

        private FilterDefinition<OwnerDataModel> BuildMongoFilterSentence(QueryFilterSentence queryFilterSentence)
        {
            if (string.IsNullOrWhiteSpace(queryFilterSentence?.FieldName))
            {
                return Builders<OwnerDataModel>.Filter.Empty;
            }

            var property = typeof(OwnerDataModel).GetProperties().FirstOrDefault(p => p.Name.ToLower().Equals(queryFilterSentence.FieldName.ToLower()));
            if (property is null)
            {
                return Builders<OwnerDataModel>.Filter.Empty;
            }

            var builder = Builders<OwnerDataModel>.Filter;
            if (queryFilterSentence.Operator == FilterOperator.Equal)
            {
                return builder.Eq(property.Name, queryFilterSentence.Value);
            }
            else if (queryFilterSentence.Operator == FilterOperator.NotEqual)
            {
                return builder.Ne(property.Name, queryFilterSentence.Value);
            }
            else if (queryFilterSentence.Operator == FilterOperator.GreaterThan)
            {
                return builder.Gt(property.Name, queryFilterSentence.Value);
            }
            else if (queryFilterSentence.Operator == FilterOperator.GreaterThanOrEqual)
            {
                return builder.Gte(property.Name, queryFilterSentence.Value);
            }
            else if (queryFilterSentence.Operator == FilterOperator.LessThan)
            {
                return builder.Lt(property.Name, queryFilterSentence.Value);
            }
            else if (queryFilterSentence.Operator == FilterOperator.LessThanOrEqual)
            {
                return builder.Lte(property.Name, queryFilterSentence.Value);
            }
            else if (queryFilterSentence.Operator == FilterOperator.Contains)
            {
                return builder.Regex(property.Name, new BsonRegularExpression(queryFilterSentence.Value!.ToString(), "i"));
            }
            else if (queryFilterSentence.Operator == FilterOperator.NotContains)
            {
                return builder.Not(builder.Regex(property.Name, new BsonRegularExpression(queryFilterSentence.Value!.ToString(), "i")));
            }
            else if (queryFilterSentence.Operator == FilterOperator.StartsWith)
            {
                return builder.Regex(property.Name, new BsonRegularExpression($"^{queryFilterSentence.Value}", "i"));
            }
            else if (queryFilterSentence.Operator == FilterOperator.NotStartsWith)
            {
                return builder.Not(builder.Regex(property.Name, new BsonRegularExpression($"^{queryFilterSentence.Value}", "i")));
            }
            else if (queryFilterSentence.Operator == FilterOperator.EndsWith)
            {
                return builder.Regex(property.Name, new BsonRegularExpression($"{queryFilterSentence.Value}$", "i"));
            }
            else if (queryFilterSentence.Operator == FilterOperator.NotEndsWith)
            {
                return builder.Not(builder.Regex(property.Name, new BsonRegularExpression($"{queryFilterSentence.Value}$")));
            }

            return Builders<OwnerDataModel>.Filter.Empty;
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
