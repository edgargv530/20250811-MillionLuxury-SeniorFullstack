using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MillionLuxury.TechhicalTest.Application.UseCases.Owners;
using System.Reflection;

namespace MillionLuxury.TechhicalTest.Application
{
    public static class ApplicationConfiguration
    {
        public static IEnumerable<Type> GetProfiles()
        {
            var profiles = Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(Profile).IsAssignableFrom(t)).ToList();
            return profiles;
        }

        public static void AddUseCases(this IServiceCollection services)
        {
            services
                .AddTransient<IOwnersUseCase, OwnersUseCase>();
        }
    }
}
