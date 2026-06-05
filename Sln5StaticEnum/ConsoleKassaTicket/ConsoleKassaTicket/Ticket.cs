using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleKassaTicket
{
    internal class Ticket
    {
        public List<Product> Producten { get; set; }
        public Betaalwijze BetaaldMet { get; set; } = Betaalwijze.cash;
        public string Kassier { get; set; }

        decimal _totaalprijs;
        public decimal Totaalprijs
        {
             get
             {
                _totaalprijs = 0;

                foreach (Product p in Producten)
                {
                    _totaalprijs += p.Eenheidsprijs;
                }

                if (BetaaldMet == Betaalwijze.visa)
                {
                    _totaalprijs += Convert.ToDecimal(0.12);
                }

                return _totaalprijs;
             }
        }

        public void PrintOut()
        {
            Console.WriteLine("\nKASSATICKET");
            Console.WriteLine("===========");
            Console.WriteLine($"Uw kassier: {Kassier}\n");

            foreach (Product p in Producten)
            { 
                Console.WriteLine($"{p.ToString()}");
            }
            Console.WriteLine("-----------");
            
            if(BetaaldMet == Betaalwijze.visa)
            {
                Console.WriteLine($"Visa kosten: 0,12");
            }
            Console.WriteLine($"Totaal: {Totaalprijs} \n");
        }

    }
}
