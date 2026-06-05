using System;
using System.Collections.Generic;
using System.Text;

namespace WpfZalaStock
{
    class Sieraad : Product
    {
        public MateriaalType Materiaal { get; set; }
        
        public Sieraad(string naam, string merk, double prijs, string kleur, int aantalInStock, MateriaalType materiaal) : base(naam, merk, prijs, kleur, aantalInStock)
        {
            Materiaal = materiaal;
        }
    }
}
