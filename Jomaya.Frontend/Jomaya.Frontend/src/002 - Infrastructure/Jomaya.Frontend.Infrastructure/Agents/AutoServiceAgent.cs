using Jomaya.Frontend.Entities;
using Jomaya.Frontend.Infrastructure.Agents.AutoService;
using Jomaya.Frontend.Infrastructure.Agents.AutoService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jomaya.Frontend.Infrastructure.Agents.Service
{
    public class AutoServiceAgent : IAutoServiceAgent
    {
        public Entities.Auto AutoInvoeren(Entities.Auto auto)
        {
            using (IAutoServiceClient agent = new AutoServiceClient())
            {
                agent.BaseUri = new Uri("http://jomaya-autoservice/");
                //agent.BaseUri = new Uri("http://localhost:5003/");
                var result = agent.PostCreateAuto(ObjectMapper.ConvertAuto(auto));

                if (result is AutoService.Models.Auto)
                {
                    return ObjectMapper.ConvertAuto(result as AutoService.Models.Auto);                    
                }
                else
                {
                    throw new InvalidOperationException("Een fout is opgetreden bij de MicroService, " + result.ToString());
                }
            }
        }

        public Entities.Onderhoudsopdracht OnderhoudsopdrachtInvoeren(Entities.Onderhoudsopdracht onderhoudsopdracht)
        {
            using (IAutoServiceClient agent = new AutoServiceClient())
            {
                agent.BaseUri = new Uri("http://jomaya-autoservice/");
                //agent.BaseUri = new Uri("http://localhost:5003/");
                var result = agent.PostCreateOnderhoudsopdracht(ObjectMapper.ConvertOnderhoudsOpdracht(onderhoudsopdracht));

                if (result is AutoService.Models.Onderhoudsopdracht)
                {
                    return ObjectMapper.ConvertOnderhoudsOpdracht(result as AutoService.Models.Onderhoudsopdracht);                    
                }
                else
                {
                    throw new InvalidOperationException("Een fout is opgetreden bij de MicroService, " + (result as System.Exception).Message);
                }
            }
        }

        public Entities.Onderhoudsopdracht OnderhoudsOpdrachtUpdate(Entities.Onderhoudsopdracht onderhoudsopdracht)
        {
            using (IAutoServiceClient agent = new AutoServiceClient())
            {
                agent.BaseUri = new Uri("http://jomaya-autoservice/");
                //agent.BaseUri = new Uri("http://localhost:5003/");
                var result = agent.PutUpdateOnderhoudsopdracht(ObjectMapper.ConvertOnderhoudsOpdracht(onderhoudsopdracht));

                if (result is AutoService.Models.Onderhoudsopdracht)
                {
                    return ObjectMapper.ConvertOnderhoudsOpdracht(result as AutoService.Models.Onderhoudsopdracht);
                }
                else
                {
                    throw new InvalidOperationException("Een fout is opgetreden bij de MicroService, " + (result as ErrorMessage).FoutMelding);
                }
            }
        }
    }
}
