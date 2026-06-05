using System;
using System.Collections.Generic;
using System.Text;

namespace WpfZalaStock
{
    class Schoen : Product
    {
        public BreedteType Breedte { get; set; }
        public SluitingType Sluiting { get; set; }
        public NeusType Neus { get; set; }
        public Schoen(string naam, string merk, double prijs, string kleur, int aantalInStock, BreedteType breedte, SluitingType sluiting, NeusType neus) : base(naam, merk, prijs, kleur, aantalInStock)
        {
            Breedte = breedte;
            Sluiting = sluiting;
            Neus = neus;
        }
    }
}
