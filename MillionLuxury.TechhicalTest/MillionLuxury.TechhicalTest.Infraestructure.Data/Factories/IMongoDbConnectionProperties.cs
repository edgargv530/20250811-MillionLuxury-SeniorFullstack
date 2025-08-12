namespace MillionLuxury.TechhicalTest.Infraestructure.Data.Factories
{
    public interface IMongoDbConnectionProperties
    {
        string ConnectionString { get; }
        string DatabaseName { get; }
        string CollectionOwnerName { get; }
        string CollectionPropertiesName { get; }
    }
}
