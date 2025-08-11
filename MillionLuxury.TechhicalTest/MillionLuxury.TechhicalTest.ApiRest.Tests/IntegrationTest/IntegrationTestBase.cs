namespace MillionLuxury.TechhicalTest.ApiRest.Tests.IntegrationTest
{
    internal abstract class IntegrationTestBase
    {
        protected readonly string connectionString = "mongodb://localhost:27017";
        protected readonly string databaseName = "PropertiesDB";
    }
}
