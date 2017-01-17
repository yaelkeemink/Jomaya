using Jomaya.AutoService.Entities;
using Jomaya.Common;
using Minor.WSA.Commons;

namespace Jomaya.AutoService.Outgoing.Events {
    public class OnderhoudStatusChangedEvent : DomainEvent
    {
        public long OnderhoudsopdrachtId { get; set; }
        public OnderhoudStatus Status { get; set; }
    }
}
