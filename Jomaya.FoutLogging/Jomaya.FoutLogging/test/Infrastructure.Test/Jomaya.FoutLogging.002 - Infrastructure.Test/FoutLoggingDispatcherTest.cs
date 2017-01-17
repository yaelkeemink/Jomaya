using Jomaya.Common;
using Jomaya.Common.Events;
using Jomaya.FoutLogging.Infrastructure;
using Jomaya.FoutLogging.Infrastructure.DAL;
using Jomaya.FoutLogging.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.WSA.Commons;
using Minor.WSA.EventBus;
using Minor.WSA.EventBus.Config;
using Minor.WSA.EventBus.Dispatcher;
using Minor.WSA.EventBus.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Jomaya.FoutLogging._002___Infrastructure.Test
{

    [TestClass]
    public class FoutLoggingDispatcherTest
    {
        private DbContextOptions _options;
        private EventBusConfig _config;
        private static DbContextOptions<FoutLoggingContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<FoutLoggingContext>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        [TestInitialize]
        public void Init()
        {
            _options = CreateNewContextOptions();

            _config = new EventBusConfig()
            {
                Host = "localhost",
                Port = 5673,
                QueueName = "DispatcherTest"
            };

        }
        
        [TestMethod]
        public void DispatcherTest()
        {        
            using (var eventSender = new EventPublisher(_config))
            using (FoutLoggingContext context = new FoutLoggingContext(_options))
            using (CustomExceptionRepository repo = new CustomExceptionRepository(context))
            using (FoutLoggingDispatcher dispatcher = new FoutLoggingDispatcher(_config, repo))
            {

                var myException = new ExceptionThrownEvent()
                {
                    ExceptionType = typeof(ArgumentException),
                    GUID = Guid.NewGuid().ToString(),
                    Message = "Dit is een TestException",
                    TimeStamp = DateTime.Now,
                    RoutingKey = "jomaya.exception.Test"
                };

                eventSender.Publish(myException);

                Thread.Sleep(3000);

                 Assert.IsTrue(context.Exceptions.Count(c => c.ExceptionType == typeof(ArgumentException).ToString()) > 0);

                var result = context.Exceptions.First(c => c.ExceptionType == typeof(ArgumentException).ToString());

                Assert.AreEqual("Dit is een TestException", result.Message);                
            }
        }


        [TestMethod]
        public void TestCommonExceptionPublisher()
        {
            var errorMessage = "Dit is een TestException";            

            var myException = new ArgumentException(errorMessage);              

            using (FoutLoggingContext context = new FoutLoggingContext(_options))
            using (CustomExceptionRepository repo = new CustomExceptionRepository(context))
            using (FoutLoggingDispatcher dispatcher = new FoutLoggingDispatcher(_config, repo))
            {
                ExceptionEventPublisher.PublishException(myException, false);

                Thread.Sleep(10000);         

                Assert.IsTrue(context.Exceptions.Count(c => c.ExceptionType == typeof(ArgumentException).ToString()) > 0);

                var result = context.Exceptions.First(c => c.ExceptionType == typeof(ArgumentException).ToString());

                Assert.AreEqual("Dit is een TestException", result.Message);
            }
        }
    }
}
