using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleStaticEnumOefenblad.Exercises.Classes
{
    internal class Bestelling
    {
        public string KlantNaam { get; set; }
        public string ProductNaam { get; set; }
        public BestelStatus Status { get; set; } = new BestelStatus();

        public bool KanNogGewijzigdWorden
        {
            get
            {
                return Status != BestelStatus.Geleverd || Status != BestelStatus.Geannuleerd;
            }
        }

        public override string ToString()
        {
            return $"Klant: {KlantNaam}, Product: {ProductNaam}, Status: {Status}";
        }

    }
}
