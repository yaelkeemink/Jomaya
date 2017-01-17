using System;
using Minor.WSA.Commons;
using Jomaya.IntegratieService.Entities;
using System.Text;
using System.IO;
using System.Net;
using Jomaya.Common.Events;
using System.Globalization;
using Jomaya.Common;

namespace Jomaya.IntegratieService.Services
{
    public class RDWService : IDisposable
    {
        private readonly IEventPublisher _publisher;

        public RDWService(IEventPublisher publisher)
        {
            _publisher = publisher;
        }

        public void Createmessage(AutoKlaargemeldEvent autoKlaargemeldEvent, Garage garage)
        {
            try
            {
                string message = @"<apkKeuringsverzoekRequestMessage xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
                                <keuringsverzoek xmlns='http://www.rdw.nl' xmlns:apk='http://www.rdw.nl/apk' correlatieId='51a33867-b5ec-4f10-9b31-b4e3ddcb58eb'>" +
                                        $"<voertuig type='personenauto'>" +
                                            $"<kenteken>{autoKlaargemeldEvent.Kenteken.Replace(" ", "")}</kenteken>" +
                                            $"<kilometerstand>{autoKlaargemeldEvent.KilometerStand}</kilometerstand>" +
                                            $"<naam>{autoKlaargemeldEvent.EigenaarNaam}</naam>" +
                                        $"</voertuig>" +
                                            $"<apk:keuringsdatum>{DateTime.Now.ToString("yyyy-MM-dd")}</apk:keuringsdatum>" +
                                            $"<apk:keuringsinstantie type='garage' kvk='3013 5370'>" +
                                            $"<apk:naam>{garage.GarageNaam}</apk:naam>" +
                                            $"<apk:plaats>{garage.PlaatsNaam}</apk:plaats>" +
                                        @"</apk:keuringsinstantie>
                                </keuringsverzoek>
                            </apkKeuringsverzoekRequestMessage>";
                string url = "http://rdwserver:18423/rdw/APKKeuringsverzoek";

                string response = PostMessage(url, message);
                APKKeuringsregistratieEvent apkKeuringsregistratieEvent = CreateEvent(response, autoKlaargemeldEvent);
                _publisher.Publish(apkKeuringsregistratieEvent);
            }
            catch(Exception e)
            {
                ExceptionEventPublisher.PublishException(e);
            }
        }

        private static APKKeuringsregistratieEvent CreateEvent(string response, AutoKlaargemeldEvent autoKlaargemeldEvent)
        {
            try
            {
                var apkKeuringsregistratieEvent = new APKKeuringsregistratieEvent() { GUID = Guid.NewGuid().ToString(), RoutingKey = "Jomaya.Onderhoudsopdracht.Updated", TimeStamp = DateTime.UtcNow };
                apkKeuringsregistratieEvent.Kenteken = response.Substring(response.IndexOf("<kenteken>") + 10, response.IndexOf("</kenteken>") - (response.IndexOf("<kenteken>") + 10));
                var steekproef = !response.Contains("<apk:steekproef xsi:nil=\"true\"/>");
                if (steekproef)
                {
                    var steekproefDatum = response.Substring(response.IndexOf("<apk:steekproef>") + 16, response.IndexOf("</apk:steekproef>") - (response.IndexOf("<apk:steekproef>") + 16));
                    apkKeuringsregistratieEvent.SteekproefDatum = DateTime.Parse(steekproefDatum);
                    apkKeuringsregistratieEvent.Status = Common.OnderhoudStatus.InSteekproef;
                }
                else
                {
                    apkKeuringsregistratieEvent.Status = Common.OnderhoudStatus.Afgemeld;
                }
                apkKeuringsregistratieEvent.AutoId = autoKlaargemeldEvent.AutoId;
                apkKeuringsregistratieEvent.OnderhoudId = autoKlaargemeldEvent.OnderhoudId;
                apkKeuringsregistratieEvent.Kilometerstand = autoKlaargemeldEvent.KilometerStand;
                apkKeuringsregistratieEvent.Werkzaamheden = autoKlaargemeldEvent.Werkzaamheden;
                apkKeuringsregistratieEvent.OnderhoudsDatum = autoKlaargemeldEvent.OnderhoudsDatum;
                return apkKeuringsregistratieEvent;
            }
            catch(Exception e)
            {
                ExceptionEventPublisher.PublishException(e);
                return null;
            }
        }

        private string PostMessage(string url, string message)
        {
            try
            {
                byte[] bodyBytes = Encoding.UTF8.GetBytes(message);

                WebRequest request = HttpWebRequest.Create(url);

                request.Method = "POST";

                request.ContentType = "text/xml";

                Stream requestStream = request.GetRequestStreamAsync().Result;

                requestStream.Write(bodyBytes, 0, bodyBytes.Length);

                WebResponse response = request.GetResponseAsync().Result;

                StreamReader reader = new StreamReader(response.GetResponseStream());

                string returnMessage = reader.ReadToEnd();

                return returnMessage;
            }
            catch (Exception e)
            {
                ExceptionEventPublisher.PublishException(e);
                return null;
            }
        }

        public void Dispose()
        {
        }
    }
}
