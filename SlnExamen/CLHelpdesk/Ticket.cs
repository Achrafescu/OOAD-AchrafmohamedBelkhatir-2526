using CLHelpdesk;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLHelpdesk
{
    public class Ticket
    {
        // Properties
        public int Id { get; set; }
        public string Titel { get; set; }
        public Medewerker Melder { get; set; }
        public TicketPrioriteit Prioriteit { get; set; }
        public bool IsAfgesloten { get; set; }
        public DateTime DatumAangemaakt { get; set; }
        public DateTime? DatumAfgesloten { get; set; }

        // Constructor
        public Ticket(int id, string titel, Medewerker melder, TicketPrioriteit prioriteit, DateTime datumAangemaakt)
        {
            Id = id;
            Titel = titel;
            Melder = melder;
            Prioriteit = prioriteit;
            IsAfgesloten = false;
            DatumAangemaakt = datumAangemaakt;
            DatumAfgesloten = null;
        }

        // Geeft korte info voor in de ListBox
        public override string ToString()
        {
            string status = IsAfgesloten ? "✓" : "⏳";
            return string.Format("[{0}] {1} - {2} ({3})", status, Id, Titel, Prioriteit);
        }

        // Geeft volledige info voor in de detailweergave
        public virtual string GeefInfo()
        {
            string afgesloten = IsAfgesloten
                ? DatumAfgesloten.Value.ToString("yyyy-MM-dd HHmm")
                : "Nog open";

            return string.Format(
                "ID: {0}\nTitel: {1}\nMelder: {2} {3}\nPrioriteit: {4}\nStatus: {5}\nAangemaakt: {6}\nAfgesloten: {7}",
                Id,
                Titel,
                Melder.Voornaam,
                Melder.Achternaam,
                Prioriteit,
                IsAfgesloten ? "Afgesloten" : "Open",
                DatumAangemaakt.ToString("yyyy-MM-dd HHmm"),
                afgesloten
            );
        }
    }
}

