using Jomaya.Frontend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jomaya.Frontend.Infrastructure.Agents
{
    public interface IKlantServiceAgent
    {
        Klant KlantInvoeren(Klant klant);
    }
}
