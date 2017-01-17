using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Jomaya.Frontend.Infrastructure.DAL;
using Jomaya.Frontend.Infrastructure.DAL.Repositories;
using Jomaya.Frontend.Entities;

namespace Jomaya.Frontend._002___Infrastructure.Test
{
    [TestClass]
    public class KlantDALTest
    {
        private DbContextOptions _options;
        private static DbContextOptions<FrontEndContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<FrontEndContext>();
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
        public void TestAdd()
        {

            using (var repo = new KlantRepository(new FrontEndContext(_options)))
            {
                repo.Insert(new Klant()
                {
                    Voorletters = "Y.P",
                    Achternaam = "Keemink"
                });
            }


            using (var repo = new KlantRepository(new FrontEndContext(_options)))
            {
                Assert.AreEqual(1, repo.Count());
            }
        }

        [TestMethod]
        public void TestFind()
        {
            using (var repo = new KlantRepository(new FrontEndContext(_options)))
            {
                repo.Insert(new Klant()
                {
                    Voorletters = "Y.P",
                    Achternaam = "Keemink"
                });
            }

            using (var repo = new KlantRepository(new FrontEndContext(_options)))
            {
                var result = repo.Find(1);
                Assert.AreEqual(1, result.Id);
                Assert.AreEqual("Keemink", result.Achternaam);
                Assert.AreEqual("Y.P", result.Voorletters);
            }
        }
        [TestMethod]
        public void TestDelete()
        {
            using (var repo = new KlantRepository(new FrontEndContext(_options)))
            {
                Klant klant = new Klant()
                {
                    Voorletters = "Y.P",
                    Achternaam = "Keemink"
                };
                repo.Insert(klant);
                Assert.AreEqual(1, repo.Count());
                repo.Delete(1);
            }

            using (var repo = new KlantRepository(new FrontEndContext(_options)))
            {
                Assert.AreEqual(0, repo.Count());
            }
        }
        [TestMethod]
        public void TestFindAll()
        {
            using (var repo = new KlantRepository(new FrontEndContext(_options)))
            {
                Klant klant = new Klant()
                {
                    Voorletters = "Y.P",
                    Achternaam = "Keemink"
                };
                repo.Insert(klant);
                klant = new Klant()
                {
                    Voorletters = "Y.P",
                    Achternaam = "Keemink2"
                };
                repo.Insert(klant);
            }

            using (var repo = new KlantRepository(new FrontEndContext(_options)))
            {
                Assert.AreEqual(2, repo.Count());
            }
        }
    }
}
