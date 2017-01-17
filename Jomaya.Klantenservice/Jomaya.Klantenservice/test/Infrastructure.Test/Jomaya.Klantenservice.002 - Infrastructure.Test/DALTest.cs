using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Jomaya.Klantenservice.Infrastructure.Repositories;
using Jomaya.Klantenservice.Infrastructure.DAL;
using Jomaya.Klantenservice.Entities;

namespace Jomaya.Klantenservice._002___Infrastructure.Test
{
    [TestClass]
    public class DALTest
    {
        private DbContextOptions _options;
        private static DbContextOptions<KlantContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<KlantContext>();
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

            using (var repo = new KlantRepository(new KlantContext(_options)))
            {
                repo.Insert(new Klant()
                {
                    Voorletters = "YP"
                });
            }


            using (var repo = new KlantRepository(new KlantContext(_options)))
            {
                Assert.AreEqual(1, repo.Count());
            }
        }

        [TestMethod]
        public void TestFind()
        {
            using (var repo = new KlantRepository(new KlantContext(_options)))
            {
                repo.Insert(new Klant()
                {
                    Voorletters = "YP",
                    Tussenvoegsel = "van",
                    Achternaam = "Keemink"
                });
            }

            using (var repo = new KlantRepository(new KlantContext(_options)))
            {
                var result = repo.Find(1);
                Assert.AreEqual(1, result.Id);
                Assert.AreEqual("YP", result.Voorletters);
                Assert.AreEqual("van", result.Tussenvoegsel);
                Assert.AreEqual("Keemink", result.Achternaam);
            }
        }
        [TestMethod]
        public void TestDelete()
        {
            using (var repo = new KlantRepository(new KlantContext(_options)))
            {
                var pen = new Klant()
                {
                    Voorletters = "YP"
                };
                repo.Insert(pen);
                repo.Delete(1);
            }

            using (var repo = new KlantRepository(new KlantContext(_options)))
            {
                Assert.AreEqual(0, repo.Count());
            }
        }
        [TestMethod]
        public void TestFindAll()
        {
            using (var repo = new KlantRepository(new KlantContext(_options)))
            {
                var klant = new Klant()
                {
                    Voorletters = "YP"
                };
                repo.Insert(klant);
                klant = new Klant()
                {
                    Voorletters = "YPK"
                };
                repo.Insert(klant);
            }

            using (var repo = new KlantRepository(new KlantContext(_options)))
            {
                Assert.AreEqual(2, repo.Count());
            }
        }
    }
}
