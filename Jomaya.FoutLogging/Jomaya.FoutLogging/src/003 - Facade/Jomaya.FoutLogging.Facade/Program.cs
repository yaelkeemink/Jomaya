using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using System;
using Jomaya.FoutLogging.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;
using Jomaya.FoutLogging.Infrastructure.Repositories;
using Minor.WSA.EventBus.Config;
using Minor.WSA.EventBus.Publisher;
using Jomaya.FoutLogging.Infrastructure;

namespace Jomaya.FoutLogging.Facade
{
    public class Program : IDisposable
    {

        private static FoutLoggingDispatcher _dispatcher;
        private static FoutLoggingContext _context;
        private static CustomExceptionRepository _repo;
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
            string sqlConnectionString = "server=db;userid=jomaya;password=jomaya;database=jomaya-foutlogging;";                 

            var optionsBuilder = new DbContextOptionsBuilder<FoutLoggingContext>();
            optionsBuilder.UseMySQL(sqlConnectionString);
            _context = new FoutLoggingContext(optionsBuilder.Options);
            _repo = new CustomExceptionRepository(_context);

            var config = new EventBusConfig()
            {
                Host = "rabbitmq",
                Port = 5672,
                QueueName = "jomaya.foutlogging.queue"
            };

            var publisher = new EventPublisher(config);

            _dispatcher = new FoutLoggingDispatcher(config, _repo);

        }

        public void Dispose()
        {
            _dispatcher.Dispose();
        }
    }
}
