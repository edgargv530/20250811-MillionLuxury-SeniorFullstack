using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MillionLuxury.TechhicalTest.Domain.Repositories;
using MillionLuxury.TechhicalTest.Infraestructure.Data.Repositories;
using System.Reflection;

namespace MillionLuxury.TechhicalTest.Infraestructure.Data
{
    public static class InfrastructureDataConfiguration
    {
        public static IEnumerable<Type> GetProfiles()
        {
            var profiles = Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(Profile).IsAssignableFrom(t)).ToList();
            return profiles;
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services
                .AddTransient<IOwnerRepository, OwnerRepository>();
        }
    }
}
