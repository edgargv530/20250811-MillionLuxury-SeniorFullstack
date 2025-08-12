using MillionLuxury.TechhicalTest.Infraestructure.Data.Factories;

namespace MillionLuxury.TechhicalTest.ApiRest.Factories
{
    public class MongoDbConnectionProperties : IMongoDbConnectionProperties
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string CollectionOwnerName { get; set; } = null!;

        public string CollectionPropertiesName { get; set; } = null!;
    }
}
