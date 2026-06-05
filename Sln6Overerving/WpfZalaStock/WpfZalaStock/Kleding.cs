using System;
using System.Collections.Generic;
using System.Text;

namespace WpfZalaStock
{
    class Kleding : Product
    {
        public PasvormType Pasvorm { get; set; }
        public LengteType Lengte { get; set; }


        public Kleding(string naam, string merk, double prijs, string kleur, int aantalInStock, PasvormType pasvorm, LengteType lengte) : base(naam, merk, prijs, kleur, aantalInStock)
        {
            Pasvorm = pasvorm;
            Lengte = lengte;
        }
    }
}
