using Jomaya.AutoService.Facade;
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
        public async Task CreateAutoIntegrationTest()
        {
            // Arrange
            var _server = new TestServer(new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>());
            var _client = _server.CreateClient();

            // Act
            var response = await _client.PostAsync("api/Auto", new StringContent("{\"kenteken\": \"AA-BB-11\",\"type\":\"personenauto\" \"string\",\"klantId\": 1}", Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.AreEqual("{\"id\": 1,\"kenteken\": \"AA-BB-11\",\"type\": \"personenauto\",\"klantId\": 1}", responseString);
        }
    }
}
