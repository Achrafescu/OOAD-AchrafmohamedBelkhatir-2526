using System;
using System.Collections.Generic;
using System.Text;

namespace WpfZalaStock
{
    class Stockbeheer
    {
        public Dictionary<Product, int> Verkocht { get; set; } = new Dictionary<Product, int>();
        public Dictionary<Product, int> Geretourneerd { get; set; } = new Dictionary<Product, int>();

        public double TotaalBedragVerkocht()
        {
            double totaal = 0;
            foreach (var unitVerkocht in Verkocht)
            {
                totaal += unitVerkocht.Key.Prijs * unitVerkocht.Value;
            }
            return totaal;
        }

        public double TotaalBedragGeretourneerd()
        {
            double totaal = 0;
            foreach (var unitGeretourneerd in Geretourneerd)
            {
                totaal += unitGeretourneerd.Key.Prijs * unitGeretourneerd.Value;
            }
            return totaal;
        }

        public double TotaalBedrag()
        {
            return TotaalBedragVerkocht() - TotaalBedragGeretourneerd();
        }

        public void Verkoop(Product product, int aantal)
        {
            if (product.AantalInStock >= aantal)
            {
                product.Verkoop(aantal);
                if (Verkocht.ContainsKey(product))
                {
                    Verkocht[product] += aantal;
                }
                else
                {
                    Verkocht[product] = aantal;
                }
            }
        }

        public void Retourneren(Product product, int aantal)
        {
            product.Retourneer(aantal);
            if (Geretourneerd.ContainsKey(product))
            {
                Geretourneerd[product] += aantal;
            }
            else
            {
                Geretourneerd[product] = aantal;
            }
        }
    }
}
