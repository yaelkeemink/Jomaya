using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Jomaya.AutoService.Infrastructure.DAL;
using Jomaya.AutoService.Infrastructure.DAL.Repositories;
using Jomaya.AutoService.Entities;

namespace Jomaya.AutoService._002___Infrastructure.Test
{
    [TestClass]
    public class AutoDALTest
    {
        private DbContextOptions _options;
        private static DbContextOptions<AutosBackendContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<AutosBackendContext>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        [TestInitialize]
        public void Init()
        {
            _options = CreateNewContextOptions();
        }

        [TestMethod]
        public void TestAddAuto()
        {

            using (var repo = new AutoRepository(new AutosBackendContext(_options)))
            {
                repo.Insert(new Auto()
                {
                    Kenteken = "AA-11-BB"
                });
            }


            using (var repo = new AutoRepository(new AutosBackendContext(_options)))
            {
                var result = repo.FindAll().Single();
                Assert.AreEqual(1, repo.Count());
                Assert.AreEqual("AA-11-BB", result.Kenteken);
            }
        }

        [TestMethod]
        public void TestUpdateAuto()
        {
            //Adding
            using (var repo = new AutoRepository(new AutosBackendContext(_options)))
            {
                repo.Insert(new Auto()
                {
                    Kenteken = "AA-11-BB"
                });
            }
            //Updating
            using (var repo = new AutoRepository(new AutosBackendContext(_options)))
            {
                repo.Update(new Auto()
                {
                    Id = 1,
                    Kenteken = "AA-11-CC"
                });
            }

            //Checking
            using (var repo = new AutoRepository(new AutosBackendContext(_options)))
            {
                var result = repo.Find(1);
                Assert.AreEqual(1, repo.Count());
                Assert.AreEqual("AA-11-CC", result.Kenteken);
            }
        }

        [TestMethod]
        public void TestFindAuto()
        {
            using (var repo = new AutoRepository(new AutosBackendContext(_options)))
            {
                repo.Insert(new Auto()
                {
                    Kenteken = "AA-11-BB"
                });
            }

            using (var repo = new AutoRepository(new AutosBackendContext(_options)))
            {
                var result = repo.Find(1);
                Assert.AreEqual(1, result.Id);
                Assert.AreEqual("AA-11-BB", result.Kenteken);
            }
        }

        [TestMethod]
        public void TestFindAllAuto()
        {
            using (var repo = new AutoRepository(new AutosBackendContext(_options)))
            {
                var auto = new Auto()
                {
                    Kenteken = "AA-11-BB"
                };
                repo.Insert(auto);
                auto = new Auto()
                {
                    Kenteken = "AA-12-BB"
                };
                repo.Insert(auto);
            }

            using (var repo = new AutoRepository(new AutosBackendContext(_options)))
            {
                Assert.AreEqual(2, repo.Count());
            }
        }
    }
}
