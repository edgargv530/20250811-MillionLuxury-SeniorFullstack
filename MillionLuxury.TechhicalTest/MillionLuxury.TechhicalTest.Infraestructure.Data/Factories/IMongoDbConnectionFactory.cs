namespace MillionLuxury.TechhicalTest.Infraestructure.Data.Factories
{
    public interface IMongoDbConnectionFactory
    {
        IMongoDbConnectionProperties Create();
    }
}
