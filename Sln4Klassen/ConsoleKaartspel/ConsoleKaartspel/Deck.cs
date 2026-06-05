using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleKaartspel
{
    internal class Deck
    {
        public List<Kaart> Kaarten { get; private set; }

        public Deck()
        {
            Kaarten = new List<Kaart>();
            string[] kleuren = { "C", "D", "H", "S" };

            foreach (string kleur in kleuren)
            {
                for (int nummer = 1; nummer <= 13; nummer++)
                {
                    Kaarten.Add(new Kaart(nummer, kleur));
                }
            }

        }

        public void Schudden()
        {
            Random random = new Random();

            for (int i = 0; i < Kaarten.Count; i++)
            {
                int randomIndex = random.Next(Kaarten.Count);

                Kaart temp = Kaarten[i];
                Kaarten[i] = Kaarten[randomIndex];
                Kaarten[randomIndex] = temp;

            }

        }

        public Kaart NeemKaart()
        {
            if (Kaarten.Count == 0)
            {
                Console.WriteLine("Waarschuwing: Deck is leeg!");
                return null;
            }
            Kaart kaart = Kaarten[Kaarten.Count - 1];
            Kaarten.RemoveAt(Kaarten.Count - 1);
            return kaart;
        }

    }
}
