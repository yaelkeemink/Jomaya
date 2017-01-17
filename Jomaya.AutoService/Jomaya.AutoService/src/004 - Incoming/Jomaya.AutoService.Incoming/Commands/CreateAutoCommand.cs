using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jomaya.AutoService.Incoming.Commands
{
    public class CreateAutoCommand
        : ICommand
    {
        public Guid CommandId { get; set; }

        public DateTime Timestamp { get; set; }

        public long KlantId { get; set; }

        public string Kenteken { get; set; }
    }
}
