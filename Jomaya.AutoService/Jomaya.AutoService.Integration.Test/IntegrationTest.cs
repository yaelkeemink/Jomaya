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
        public void CreateAutoIntegrationTest()
        {
            // Arrange
            var _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            var _client = _server.CreateClient();

            // Act
            var response = _client.PostAsync("/api/Auto", new StringContent("{\"kenteken\": \"AA-BB-11\",\"type\":\"personenauto\",\"klantId\": 1}", Encoding.UTF8, "application/json")).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;

            // Assert
            Assert.IsTrue(responseString.Contains("{\"id\":"), responseString);
            Assert.IsTrue(responseString.Contains(",\"kenteken\":\"AA-BB-11\",\"type\":\"personenauto\",\"klantId\":1}"), responseString);
        }


        [TestMethod]
        public void CreateAutoIntegrationTestNotValid()
        {
            // Arrange
            var _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            var _client = _server.CreateClient();

            // Act
            var response = _client.PostAsync("/api/Auto", new StringContent("{\"kenteken\": \"someFailedString\",\"type\":\"personenauto\",\"klantId\": 1}", Encoding.UTF8, "application/json")).Result;

            // Assert
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(400, (int)response.StatusCode);
        }

        [TestMethod]
        public void CreateOnderhoudIntegrationTest()
        {
            // Arrange
            var _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            var _client = _server.CreateClient();

            // Act
            var response = _client.PostAsync("/api/Onderhoud", new StringContent("{\"autoId\":1,\"kilometerstand\":1234,\"isApk\":true,\"werkzaamheden\":\"iets\",\"status\":0,\"datum\":\"2016-12-01\"}", Encoding.UTF8, "application/json")).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;

            // Assert
            Assert.IsTrue(responseString.Contains("{\"id\":"), "1: "+responseString);
            Assert.IsTrue(responseString.Contains(",\"autoId\":1,\"auto\":null,\"kilometerstand\":1234,\"isApk\":true,\"werkzaamheden\":\"iets\",\"status\":0,\"steekproefDatum\":\"0001-01-01T00:00:00\",\"datum\":"), "2: "+responseString);
        
        }
    }    
}
