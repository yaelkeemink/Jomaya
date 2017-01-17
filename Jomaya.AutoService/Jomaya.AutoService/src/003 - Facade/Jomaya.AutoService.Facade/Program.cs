using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Jomaya.AutoService.Facade.Dispatchers;
using Minor.WSA.EventBus.Publisher;
using Minor.WSA.EventBus.Config;
using Jomaya.AutoService.Infrastructure.DAL.Repositories;
using Jomaya.AutoService.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace Jomaya.AutoService.Facade
{
    public class Program
    {
        private static AutoDispatcher _autoDispatcher;
        private static AutoRepository _autoRepo;
        private static OnderhoudRepository _onderhoudRepo;
        private static AutosBackendContext _context;
        private static EventPublisher _publisher;
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            string sqlConnectionString = "server=db;userid=jomaya;password=jomaya;database=jomaya-autoservice;";

            _publisher = new EventPublisher(
            
              new EventBusConfig()
                {
                    Host = "rabbitmq",
                    Port = 5672,
                }                
            );

            var optionsBuilder = new DbContextOptionsBuilder<AutosBackendContext>();
            optionsBuilder.UseMySQL(sqlConnectionString);
            _context = new AutosBackendContext(optionsBuilder.Options);

            _autoRepo = new AutoRepository(_context);
            _onderhoudRepo = new OnderhoudRepository(_context);
            var config = new EventBusConfig()
            {
                Host = "rabbitmq",
                Port = 5672,
                QueueName = "jomaya.autoservice.auto.dispatcher"
            };

            var publisher = new EventPublisher(config);

            _autoDispatcher = new AutoDispatcher(config, new Services.AutoService(_autoRepo, _onderhoudRepo, _publisher));

            host.Run();
        }
    }
}
