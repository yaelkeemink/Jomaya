using Jomaya.AutoService.Entities;
using Jomaya.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jomaya.AutoService.Incoming.Commands
{
    public class UpdateOnderhoudsopdrachtCommand
        : ICommand
    {
        public Guid CommandId { get; set; }

        public DateTime Timestamp { get; set; }

        public long Id { get; set; }

        public OnderhoudStatus Status { get; set; }
    }
}
