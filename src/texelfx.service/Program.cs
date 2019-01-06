using System;
using Microsoft.Extensions.DependencyInjection;
using texelfx.library.Interfaces;
using texelfx.service.Implementations;

namespace texelfx.service
{
    public class Application
    {
        public IServiceProvider Services { get; set; }
        public IServiceWrapper ServiceWrapper { get; set; }
        public Application(IServiceCollection serviceCollection)
        {
            ConfigureServices(serviceCollection);
            Services = serviceCollection.BuildServiceProvider();
            ServiceWrapper = Services.GetRequiredService<IServiceWrapper>();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IServiceWrapper, ServiceWrapper>();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            
            Application application = new Application(serviceCollection);

            application.ConfigureServices(serviceCollection);

            application.ServiceWrapper.Start();
        }
    }
}
























>>>>>>> .theirs
