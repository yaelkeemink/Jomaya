using System;
using Jomaya.Frontend.Entities;
using Jomaya.Frontend.Infrastructure.Agents;
using Jomaya.Frontend.Infrastructure.Agents.KlantService;

namespace Jomaya.Frontend.Infrastructure.Agents
{
    public class KlantServiceAgent
        : IKlantServiceAgent
    {
        public Klant KlantInvoeren(Klant klant)
        {
            using (IKlantServiceClient agent = new KlantServiceClient())
            {
                agent.BaseUri = new Uri("http://jomaya-klantenservice/");
                //agent.BaseUri = new Uri("http://localhost:4003/");
                var result = agent.Post(ObjectMapper.ConvertKlant(klant));

                if (result is Agents.KlantService.Models.Klant)
                {

                    return ObjectMapper.ConvertKlant(result as Agents.KlantService.Models.Klant);
                }
                else
                {
                    throw new InvalidOperationException("Een fout is opgetreden bij de MicroService, " + (result as System.Exception).Message);
                }
            }
        }
    }
}
