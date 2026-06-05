using System;
using System.Collections.Generic;
using System.Text;

namespace WpfZalaStock
{
    class Product
    {
        public string Naam { get; set; }
        public string Merk { get; set; }
        public double Prijs { get; set; }
        public string Kleur { get; set; } = string.Empty;
        public int AantalInStock { get; set; }
        public Product(string naam, string merk, double prijs, string kleur, int aantalInStock)
        {
            Naam = naam;
            Merk = merk;
            Prijs = prijs;
            Kleur = kleur;
            AantalInStock = aantalInStock;
        }
        public void Verkoop(int aantal)
        {
            if (AantalInStock >= aantal)
            {
                AantalInStock -= aantal;
            }
        }
        public void Retourneer(int aantal)
        {
            AantalInStock += aantal;
        }
        public override string ToString()
        {
            return $"{Naam} ({Merk}) - €{Prijs:F2}";
        }
    }
}
