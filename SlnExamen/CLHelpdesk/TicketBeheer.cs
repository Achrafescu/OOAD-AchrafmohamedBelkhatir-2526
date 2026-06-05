using System;
using System.Collections.Generic;
using System.Text;

namespace CLHelpdesk
{
    public class TicketBeheer
    {
            private string _csvPad;
            public List<Ticket> Tickets { get; set; }
            public List<Medewerker> Medewerkers { get; set; }

            public TicketBeheer(string csvPad)
            {
                _csvPad = csvPad;
                Tickets = new List<Ticket>();
                Medewerkers = new List<Medewerker>();
                LeesTicketsUitCsv();
            }

            // ── CSV LEZEN ──────────────────────────────────────────────

            private void LeesTicketsUitCsv()
            {
                if (!File.Exists(_csvPad)) return;

                string[] lijnen = File.ReadAllLines(_csvPad);

                for (int i = 1; i < lijnen.Length; i++)   // i=1 : sla de header over
                {
                    string lijn = lijnen[i].Trim();
                    if (lijn == "") continue;

                    string[] delen = lijn.Split(';');
                    if (delen.Length < 10) continue;

                    // Kolommen: id;titel;melderVoornaam;melderAchternaam;melderId;
                    //           prioriteit;isAfgesloten;type;extraInfo;
                    //           datumAangemaakt;datumAfgesloten

                    int id = int.Parse(delen[0]);
                    string titel = delen[1];
                    string voornaam = delen[2];
                    string achternaam = delen[3];
                    string melderId = delen[4];
                    string prioriteitTekst = delen[5];
                    bool isAfgesloten = delen[6].ToLower() == "true";
                    string typeTekst = delen[7];
                    string toestel = delen[8];
                    string datumTekstAan = delen[9];
                    string datumTekstAf = delen.Length > 10 ? delen[10] : "";

                    // Medewerker zoeken of aanmaken
                    Medewerker melder = ZoekOfMaakMedewerker(melderId, voornaam, achternaam);

                    // Prioriteit parsen
                    TicketPrioriteit prioriteit = TicketPrioriteit.Normaal;
                    if (prioriteitTekst == "Hoog") prioriteit = TicketPrioriteit.Hoog;
                    if (prioriteitTekst == "Laag") prioriteit = TicketPrioriteit.Laag;

                    // Datum aangemaakt parsen
                    DateTime datumAan = DateTime.ParseExact(datumTekstAan.Trim(), "yyyy-MM-dd HHmm", null);

                    // Ticket aanmaken op basis van type
                    Ticket ticket;
                    if (typeTekst == "Hardware")
                    {
                        ticket = new HardwareTicket(id, titel, melder, prioriteit, datumAan, toestel);
                    }
                    else
                    {
                        ticket = new SoftwareTicket(id, titel, melder, prioriteit, datumAan, toestel);
                    }

                    // Status en sluitdatum instellen
                    ticket.IsAfgesloten = isAfgesloten;
                    if (isAfgesloten && datumTekstAf.Trim() != "")
                    {
                        ticket.DatumAfgesloten = DateTime.ParseExact(datumTekstAf.Trim(), "yyyy-MM-dd HHmm", null);
                    }

                    // Ticket toevoegen aan lijsten
                    Tickets.Add(ticket);
                    melder.Tickets.Add(ticket);
                }
            }

            // ── HULPFUNCTIE : medewerker zoeken of aanmaken ───────────

            private Medewerker ZoekOfMaakMedewerker(string id, string voornaam, string achternaam)
            {
                foreach (Medewerker m in Medewerkers)
                {
                    if (m.Id == id) return m;
                }

                Medewerker nieuw = new Medewerker(id, voornaam, achternaam);
                Medewerkers.Add(nieuw);
                return nieuw;
            }

            // ── TICKET TOEVOEGEN ──────────────────────────────────────

            public void VoegTicketToe(Ticket ticket)
            {
                Tickets.Add(ticket);
                ticket.Melder.Tickets.Add(ticket);
                SchrijfAlleCsvTickets();
            }

            // ── TICKET AFSLUITEN ─────────────────────────────────────

            public void SluitTicketAf(int ticketId)
            {
                foreach (Ticket t in Tickets)
                {
                    if (t.Id == ticketId)
                    {
                        t.IsAfgesloten = true;
                        t.DatumAfgesloten = DateTime.Now;
                        break;
                    }
                }
                SchrijfAlleCsvTickets();
            }

            // ── NIEUW ID GENEREREN ────────────────────────────────────

            public int GeefNieuwId()
            {
                int maxId = 0;
                foreach (Ticket t in Tickets)
                {
                    if (t.Id > maxId) maxId = t.Id;
                }
                return maxId + 1;
            }

            // ── CSV SCHRIJVEN ─────────────────────────────────────────

            private void SchrijfAlleCsvTickets()
            {
                List<string> lijnen = new List<string>();
                lijnen.Add("id;titel;melderVoornaam;melderAchternaam;melderId;prioriteit;isAfgesloten;type;extraInfo;datumAangemaakt;datumAfgesloten");

                foreach (Ticket t in Tickets)
                {
                    string type = t is HardwareTicket ? "Hardware" : "Software";
                    string toestel = t is HardwareTicket
                        ? ((HardwareTicket)t).Toestel
                        : ((SoftwareTicket)t).Toestel;

                    string datumAf = t.DatumAfgesloten.HasValue
                        ? t.DatumAfgesloten.Value.ToString("yyyy-MM-dd HHmm")
                        : "";

                    string lijn = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10}",
                        t.Id,
                        t.Titel,
                        t.Melder.Voornaam,
                        t.Melder.Achternaam,
                        t.Melder.Id,
                        t.Prioriteit,
                        t.IsAfgesloten.ToString().ToLower(),
                        type,
                        toestel,
                        t.DatumAangemaakt.ToString("yyyy-MM-dd HHmm"),
                        datumAf
                    );
                    lijnen.Add(lijn);
                }

                File.WriteAllLines(_csvPad, lijnen.ToArray());
            }
        }
    }
