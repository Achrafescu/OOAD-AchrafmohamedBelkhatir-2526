using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleKaartspel
{
    internal class Kaart
    {
        private int _nummer;
        public int Nummer 
        { 
            get { return _nummer; }
            set
            {
                if (value < 1 || value > 13)
                {
                    throw new ArgumentOutOfRangeException("Nummer moet tussen 1 en 13 liggen.");
                }
                _nummer = value;
            } 
        }
        private string _kleur;
        public string Kleur
        { get { return _kleur; }
          set
            {
                if (value != "C" && value != "D" && value != "H" && value != "S")
                {
                    throw new ArgumentOutOfRangeException("Kleur moet 'C', 'D', 'H' of 'S' zijn.");
                }
                _kleur = value;
            }
        }

        public Kaart (int nummer, string kleur)
        {
            Nummer = nummer;
            Kleur = kleur;
        }

    }
}
