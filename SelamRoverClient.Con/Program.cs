using Microsoft.Extensions.DependencyInjection;
using SelamRoverClient.Service;
using SelamRoverClient.Service.Base;
using System;

namespace SelamRoverClient.Con
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceProvider serviceProvider = InitializeServiceProvider();

            var roverService = serviceProvider.GetService<IRoverService>();
            roverService.Run();
        }


        public static IServiceProvider InitializeServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddScoped<IRoverService, RoverService>();
            return services.BuildServiceProvider();
        }
    }
}
