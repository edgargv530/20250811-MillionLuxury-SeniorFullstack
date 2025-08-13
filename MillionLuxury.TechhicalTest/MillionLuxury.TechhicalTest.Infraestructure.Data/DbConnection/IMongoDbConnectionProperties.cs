namespace MillionLuxury.TechhicalTest.Infraestructure.Data.DbConnection
{
    public interface IMongoDbConnectionProperties
    {
        string ConnectionString { get; }
        string DatabaseName { get; }
        string CollectionOwnerName { get; }
        string CollectionPropertiesName { get; }
    }
}
