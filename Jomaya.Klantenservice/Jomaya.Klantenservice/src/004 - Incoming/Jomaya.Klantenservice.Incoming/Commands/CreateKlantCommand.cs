using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jomaya.Klantenservice.Incoming.Commands
{
    public class CreateKlantCommand
        : ICommand
    {
        public Guid CommandId { get; set; }

        public DateTime Timestamp { get; set; }

        public string Voorletters { get; set; }
    }
}
