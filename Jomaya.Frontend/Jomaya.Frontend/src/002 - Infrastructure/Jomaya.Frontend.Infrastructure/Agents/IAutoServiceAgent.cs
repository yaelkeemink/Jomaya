using Jomaya.Frontend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jomaya.Frontend.Infrastructure.Agents.Service
{
    public interface IAutoServiceAgent
    {
        Auto AutoInvoeren(Auto auto);
        Onderhoudsopdracht OnderhoudsopdrachtInvoeren(Onderhoudsopdracht onderhoudsopdracht);
        Onderhoudsopdracht OnderhoudsOpdrachtUpdate(Onderhoudsopdracht onderhoudsopdracht);

    }
}
