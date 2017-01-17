using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jomaya.AutoService.Incoming.Commands
{
    public class CreateOnderhoudsopdrachtCommand
        : ICommand
    {
        public Guid CommandId { get; set; }

        public DateTime Timestamp { get; set; }

        public int Kilometerstand { get; set; }

        public long AutoId { get; set; }
    }
}
