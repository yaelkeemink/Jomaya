using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Jomaya.Frontend.Facade.Dispatchers;
using Jomaya.Frontend.Infrastructure.DAL;
using Jomaya.Frontend.Infrastructure.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;
using Minor.WSA.EventBus.Config;
using Minor.WSA.EventBus.Publisher;

namespace Jomaya.Frontend.Facade
{
    public class Program
    {
        private static FrontEndContext _context;

        private static KlantDispatcher _klantDispatcher;
        private static AutoDispatcher _autoDispatcher;

        private static KlantRepository _klantRepo;
        private static AutoRepository _autoRepo;
        private static OnderhoudRepository _onderhoudsRepo;
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
            initializeEventDispatcher();
            host.Run();
        }

        private static void initializeEventDispatcher()
        {
            string sqlConnectionString = "server=db;userid=jomaya;password=jomaya;database=jomaya-frontend;";

            var optionsBuilder = new DbContextOptionsBuilder<FrontEndContext>();
            optionsBuilder.UseMySQL(sqlConnectionString);
            _context = new FrontEndContext(optionsBuilder.Options);

            _klantRepo = new KlantRepository(_context);
            _autoRepo = new AutoRepository(_context);
            _onderhoudsRepo = new OnderhoudRepository(_context);

            var config = new EventBusConfig()
            {
                Host = "rabbitmq",
                Port = 5672,
                QueueName = "jomaya.frontend.auto.dispatcher"
            };

            var publisher = new EventPublisher(config);

            _autoDispatcher = new AutoDispatcher(config, _autoRepo, _onderhoudsRepo);

            config = new EventBusConfig()
            {
                Host = "rabbitmq",
                Port = 5672,
                QueueName = "jomaya.frontend.klant.dispatcher"
            };
            _klantDispatcher = new KlantDispatcher(config, _klantRepo);
        }

        public void Dispose()
        {
            _klantDispatcher.Dispose();
            _autoDispatcher.Dispose();
            _klantRepo.Dispose();
            _autoRepo.Dispose();
            _onderhoudsRepo.Dispose();
            _context.Dispose();
        }
    }
}