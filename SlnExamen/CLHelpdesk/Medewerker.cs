using System;
using System.Collections.Generic;
using System.Text;

namespace CLHelpdesk
{
    public class Medewerker
    {
        // Properties
        public string Id { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public List<Ticket> Tickets { get; set; }

        // Constructor
        public Medewerker(string id, string voornaam, string achternaam)
        {
            Id = id;
            Voornaam = voornaam;
            Achternaam = achternaam;
            Tickets = new List<Ticket>();
        }

        // Geeft volledige naam
        public override string ToString()
        {
            return string.Format("{0} {1}", Voornaam, Achternaam);
        }
    }
}
