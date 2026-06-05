using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace CLHelpdesk
{
    public class HardwareTicket : Ticket
    {
        public string Toestel { get; set; }

        public HardwareTicket(int id, string titel, Medewerker melder, TicketPrioriteit prioriteit, DateTime datumAangemaakt, string toestel)
            : base(id, titel, melder, prioriteit, datumAangemaakt)
        {
            Toestel = toestel;
        }

        public override string GeefInfo()
        {
            string afgesloten = IsAfgesloten
                ? DatumAfgesloten.Value.ToString("yyyy-MM-dd HHmm")
                : "Nog open";

            return string.Format(
                "Titel: {0}\nMelder: {1} {2}\nPrioriteit: {3}\nStatus: {4}\nAangemaakt: {5}\nAfgesloten: {6}\nType: Hardware\nToestel: {7}",
                Titel,
                Melder.Voornaam,
                Melder.Achternaam,
                Prioriteit,
                IsAfgesloten ? "Afgesloten" : "Open",
                DatumAangemaakt.ToString("yyyy-MM-dd HHmm"),
                afgesloten,
                Toestel
            );
        }

        public override string ToString()
        {
            string status = IsAfgesloten ? "✓" : "⏳";
            return string.Format("[{0}] {1} - {2} (Hardware)", status, Id, Titel);
        }
    }
}
