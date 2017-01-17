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
using Jomaya.Common;

namespace Jomaya.AutoService._002___Infrastructure.Test
{
    [TestClass]
    public class OnderhoudDALTest
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
        public void TestAddOnderhoud()
        {

            using (var repo = new OnderhoudRepository(new AutosBackendContext(_options)))
            {
                repo.Insert(new Onderhoudsopdracht()
                {
                    IsApk = true,
                    Kilometerstand = 110000,
                    AutoId = 1,
                    Status = OnderhoudStatus.Aangemeld,
                    Werkzaamheden = "Niks",
                });
            }


            using (var repo = new OnderhoudRepository(new AutosBackendContext(_options)))
            {
                var result = repo.Find(1);
                Assert.AreEqual(1, repo.Count());
                Assert.AreEqual(110000, result.Kilometerstand);
                Assert.AreEqual(1, result.AutoId);
                Assert.IsTrue(result.IsApk);
                Assert.AreEqual(OnderhoudStatus.Aangemeld, result.Status);
                Assert.AreEqual("Niks", result.Werkzaamheden);
            }
        }

        [TestMethod]
        public void TestUpdateOnderhoud()
        {
            //Adding
            using (var repo = new OnderhoudRepository(new AutosBackendContext(_options)))
            {
                repo.Insert(new Onderhoudsopdracht()
                {
                    IsApk = true,
                    Kilometerstand = 110000,
                    AutoId = 1,
                    Status = OnderhoudStatus.Aangemeld,
                    Werkzaamheden = "Niks",
                });
            }
            //Updating
            using (var repo = new OnderhoudRepository(new AutosBackendContext(_options)))
            {
                repo.Update(new Onderhoudsopdracht()
                {
                    Id = 1,
                    IsApk = true,
                    Kilometerstand = 110000,
                    AutoId = 1,
                    Status = OnderhoudStatus.Opgepakt,
                    Werkzaamheden = "Niks",
                });
            }

            //Checking
            using (var repo = new OnderhoudRepository(new AutosBackendContext(_options)))
            {
                var result = repo.Find(1);
                Assert.AreEqual(1, repo.Count());
                Assert.AreEqual(110000, result.Kilometerstand);
                Assert.AreEqual(1, result.AutoId);
                Assert.IsTrue(result.IsApk);
                Assert.AreEqual(OnderhoudStatus.Opgepakt, result.Status);
                Assert.AreEqual("Niks", result.Werkzaamheden);
            }
        }

        [TestMethod]
        public void TestFindOnderhoud()
        {
            using (var repo = new OnderhoudRepository(new AutosBackendContext(_options)))
            {
                repo.Insert(new Onderhoudsopdracht()
                {
                    IsApk = true,
                    Kilometerstand = 110000,
                    AutoId = 1,
                    Status = OnderhoudStatus.Aangemeld,
                    Werkzaamheden = "Niks",
                });
            }

            using (var repo = new OnderhoudRepository(new AutosBackendContext(_options)))
            {
                var result = repo.Find(1);
                Assert.AreEqual(1, repo.Count());
                Assert.AreEqual(110000, result.Kilometerstand);
                Assert.AreEqual(1, result.AutoId);
                Assert.IsTrue(result.IsApk);
                Assert.AreEqual(OnderhoudStatus.Aangemeld, result.Status);
                Assert.AreEqual("Niks", result.Werkzaamheden);
            }
        }

        [TestMethod]
        public void TestFindAllOnderhoud()
        {
            using (var repo = new OnderhoudRepository(new AutosBackendContext(_options)))
            {
                var onderhoud = new Onderhoudsopdracht()
                {
                    IsApk = true,
                    Kilometerstand = 110000,
                    AutoId = 1,
                    Status = OnderhoudStatus.Aangemeld,
                };
                repo.Insert(onderhoud);
                onderhoud = new Onderhoudsopdracht()
                {
                    IsApk = false,
                    Kilometerstand = 200000,
                    AutoId = 1,
                    Status = OnderhoudStatus.Aangemeld,
                };
                repo.Insert(onderhoud);
            }

            using (var repo = new OnderhoudRepository(new AutosBackendContext(_options)))
            {
                Assert.AreEqual(2, repo.Count());
            }
        }
    }
}
