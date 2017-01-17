using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using System;
using Minor.WSA.EventBus.Config;
using Jomaya.IntegratieService.Facade.Dispatchers;
using Jomaya.IntegratieService.Services;
using Minor.WSA.EventBus.Publisher;

namespace Jomaya.IntegratieService.Facade
{
    public class Program : IDisposable
    {

        private static RDWDispatcher _dispatcher;
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            InitializeDispatcher();

            host.Run();
        }

        private static void InitializeDispatcher()
        {
            var config = new EventBusConfig()
            {
                Host = "rabbitmq",
                Port = 5672,
                QueueName = "jomaya.rdwservice.queue"
            };
            EventPublisher publisher = new EventPublisher(config);
            RDWService service = new RDWService(publisher);

            _dispatcher = new RDWDispatcher(config, service);
          
        }

        public void Dispose()
        {
            _dispatcher.Dispose();
        }
    }
}
