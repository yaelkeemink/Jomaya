namespace Jomaya.Klantenservice.Entities
{
    public class Klant
    {
        public long Id { get; set; }

        public string Voorletters { get; set; }
        public string Tussenvoegsel { get; set; }
        public string Achternaam { get; set; }
        public string Telefoonnummer { get; set; }
    }
}
