using Minor.WSA.EventBus.Dispatcher;
using Minor.WSA.EventBus.Config;
using Jomaya.IntegratieService.Entities;
using Jomaya.IntegratieService.Services;
using System.IO;
using System.Text;
using Jomaya.Common.Events;
using System;
using Jomaya.IntegratieService.Infrastructure.Agents.KlantService;
using Jomaya.IntegratieService.Infrastructure.Agents.KlantService.Models;

namespace Jomaya.IntegratieService.Facade.Dispatchers
{
    [RoutingKey("#")]
    public class RDWDispatcher : EventDispatcher 
    {
        private RDWService _service;
        public string LogFilePath { get; set; } = "/log/logfile.txt";
        public Uri Uri { get; set; } = new Uri("http://jomaya-klantenservice/");

        public RDWDispatcher(EventBusConfig config, RDWService service) : base(config) 
        {
            _service = service;
            
        }

        public void OnAutoAfgemeld(AutoKlaargemeldEvent autoKlaargemeldEvent) 
        {
            if (autoKlaargemeldEvent.IsApk) {
                var garage = new Garage()
                {
                    GarageNaam = "Jomaya",
                    PlaatsNaam = "Utrecht",
                };
                autoKlaargemeldEvent.EigenaarNaam = GetEigenaarNaamFromKlantService(autoKlaargemeldEvent.KlantId);
                _service.Createmessage(autoKlaargemeldEvent, garage);

                var builder = new StringBuilder();

                builder.Append($"TimeStamp: {autoKlaargemeldEvent.TimeStamp} ");
                builder.Append($"Guid: {autoKlaargemeldEvent.GUID} ");
                builder.Append($"AutoID: {autoKlaargemeldEvent.AutoId} ");
                builder.Append($"Eigenaar: {autoKlaargemeldEvent.EigenaarNaam} ");
                builder.Append($"Kenteken: {autoKlaargemeldEvent.Kenteken} ");
                builder.Append($"KilometerStand: {autoKlaargemeldEvent.KilometerStand} ");
                builder.Append($"VoertuigType: personenauto ");

                LogToFile(builder.ToString());
            }
        }

        private string GetEigenaarNaamFromKlantService(long klantId)
        {
            using (IKlantServiceClient agent = new KlantServiceClient())
            {                
                agent.BaseUri = Uri; 
                var result = agent.GetKlant(klantId);

                if (result is Klant)
                {
                    string eigenaarNaam = $"{(result as Klant).Voorletters} {(result as Klant).Tussenvoegsel} {(result as Klant).Achternaam}";
                    return eigenaarNaam;
                }
                else
                {
                    throw new InvalidOperationException("Een fout is opgetreden bij de MicroService, " + (result as Infrastructure.Agents.KlantService.Models.ErrorMessage).FoutMelding);
                }
            }
        }

        private void LogToFile(string text)
        {
            if (LogFilePath == "") return;

            if (!File.Exists(LogFilePath)) File.Create(LogFilePath);

            using (var logFile = System.IO.File.Open(LogFilePath, FileMode.Append))
            using (var logWriter = new System.IO.StreamWriter(logFile))
            {
                 logWriter.WriteLine(text);
            }
               
        }
    }
}
