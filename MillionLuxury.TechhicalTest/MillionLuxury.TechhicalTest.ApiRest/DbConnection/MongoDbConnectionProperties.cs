using MillionLuxury.TechhicalTest.Infraestructure.Data.DbConnection;

namespace MillionLuxury.TechhicalTest.ApiRest.DbConnection
{
    public class MongoDbConnectionProperties : IMongoDbConnectionProperties
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string CollectionOwnerName { get; set; } = null!;

        public string CollectionPropertiesName { get; set; } = null!;
    }
}
