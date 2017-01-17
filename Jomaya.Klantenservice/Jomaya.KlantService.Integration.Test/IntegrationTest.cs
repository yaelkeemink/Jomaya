using Jomaya.Klantenservice.Facade;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jomaya.AutoService.Integration.Test
{
    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        public void CreateKlantAndGetKlantIntegrationTest()
        {
            // Arrange
            var _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            var _client = _server.CreateClient();

            // Act - 1 
            var responseCreate = _client.PostAsync("/api/Klant", new StringContent("{\"voorletters\": \"J.\",\"tussenvoegsel\":\"van\",\"achternaam\": \"Huiden\",\"telefoonnummer\":\"0123456789\"}", Encoding.UTF8, "application/json")).Result;
            var responseCreateString = responseCreate.Content.ReadAsStringAsync().Result;

            // Assert - 1
            Assert.IsTrue(responseCreateString.Contains("{\"id\":"), responseCreateString);
            Assert.IsTrue(responseCreateString.Contains("\"voorletters\":\"J.\",\"tussenvoegsel\":\"van\",\"achternaam\":\"Huiden\",\"telefoonnummer\":\"0123456789\"}"), responseCreateString);


            // Act - 2
            var responseGet = _client.GetAsync("/api/Klant/1").Result;
            
            // Assert - 2
            Assert.IsTrue(responseGet.IsSuccessStatusCode);
        }
    }
}
