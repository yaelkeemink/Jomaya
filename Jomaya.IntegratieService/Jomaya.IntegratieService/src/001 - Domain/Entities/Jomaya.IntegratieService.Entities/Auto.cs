using Jomaya.Common;

namespace Jomaya.IntegratieService.Entities
{
    public class Auto
    {
        public long Id { get; set; }
        public string Kenteken { get; set; }
        public int KilometerStand { get; set; }
        public VoertuigTypes VoertuigType { get; set; }
    }
}
