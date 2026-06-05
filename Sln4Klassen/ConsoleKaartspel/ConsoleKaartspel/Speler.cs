using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleKaartspel
{
    internal class Speler
    {
        public string Naam { get; private set; }
        public List<Kaart> Kaarten { get; private set; }
        public bool HeeftNogKaarten { get { return Kaarten != null && Kaarten.Count > 0; } }

        public Speler(string naam)
        {
            Naam = naam;
            Kaarten = new List<Kaart>();
        }

        public Speler(string naam, List<Kaart> kaarten)
        {
            Naam = naam;
            Kaarten = kaarten;
        }

        public Kaart LegKaart()
        {
            if (Kaarten == null || Kaarten.Count == 0)
            {
                throw new InvalidOperationException($"{Naam} heeft geen kaarten meer.");
            }
            Random random = new Random();
            int randomIndex = random.Next(Kaarten.Count);
            Kaart gekozenKaart = Kaarten[randomIndex];
            Kaarten.RemoveAt(randomIndex);
            return gekozenKaart;
        }

    }
}
