using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jomaya.FoutLogging.Incoming.Commands
{
    public interface ICommand
    {
        Guid CommandId { get; set; }
        DateTime Timestamp { get; set; }
    }
}
