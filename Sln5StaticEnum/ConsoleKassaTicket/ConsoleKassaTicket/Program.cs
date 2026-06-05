namespace ConsoleKassaTicket
{

    public enum Betaalwijze {cash, bancontact, visa }


    internal class Program
    {
        static void Main(string[] args)
        {

            Product bananen = new Product("bananen", Convert.ToDecimal(1.75), "p02384");
            Product brood = new Product("brood", Convert.ToDecimal(2.10), "p01820");
            Product kaas = new Product("kaas", Convert.ToDecimal(3.99), "p45612");
            Product koffie = new Product("koffie", Convert.ToDecimal(4.10), "p98754");

            string nogShoppen = "y";
            string keuze = "y";

            do
            {
                Console.WriteLine(" --- Welkom bij KRUIDVAT ! --- \n");

                List<Product> winkelWagen = new List<Product>();

                Ticket ticket = new Ticket();

                ticket.Kassier = "Annie";

                do
                {
                    Console.Write("\n1. Bananen \n" +
                    "2. Brood \n" +
                    "3. Kaas \n" +
                    "4. Koffie \n" +
                    "Welke product wilt u kopen (typ \'n\' om te stoppen) : ");

                    keuze = Console.ReadLine();

                    switch (keuze)
                    {
                        case "1": Console.WriteLine("1 banaan toegevoegd aan winkelwagen");
                                  winkelWagen.Add(bananen);
                            break;
                        case "2": Console.WriteLine("1 brood toegevoegd aan winkelwagen");
                                  winkelWagen.Add(brood);
                            break;
                        case "3": Console.WriteLine("1 kaas toegevoegd aan winkelwagen"); 
                                  winkelWagen.Add(kaas);
                            break;
                        case "4": Console.WriteLine("1 koffie toegevoegd aan winkelwagen"); 
                                  winkelWagen.Add(koffie);
                            break;
                        default: Console.WriteLine("\nJouw winkelwagen is klaar!"); break;
                    }
                }
                while (keuze != "n");

                ticket.Producten = winkelWagen;

                Console.Write("\n1. Cash \n" +
                    "2. Bancontact \n" +
                    "3. Visa \n" +
                    "Hoe wilt u betalen: ");

                string betaalKeuze = Console.ReadLine();

                switch (betaalKeuze)
                {
                    case "1": ticket.BetaaldMet = Betaalwijze.cash; break;
                    case "2": ticket.BetaaldMet = Betaalwijze.bancontact; break;
                    case "3": ticket.BetaaldMet = Betaalwijze.visa; break;
                    default: Console.WriteLine("Ongeldige keuze"); break;
                }



                ticket.PrintOut();

                Console.Write("\nWilt u terug in de winkel (y/n): ");
                nogShoppen = Console.ReadLine();
            }
            while (nogShoppen == "y");

            Console.WriteLine("\nBedankt voor uw bezoek, tot ziens !");


        }
    }
}
