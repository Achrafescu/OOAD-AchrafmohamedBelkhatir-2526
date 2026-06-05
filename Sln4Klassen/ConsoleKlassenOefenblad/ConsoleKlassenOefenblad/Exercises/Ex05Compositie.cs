using ConsoleKlassenOefenblad.Exercises.Classes;

namespace ConsoleKlassenOefenblad.Exercises;

internal class Ex05Compositie
{
    public static void Run()
    {
        Console.WriteLine("\nOefening 5: compositie");
        Console.WriteLine("-------------");
        // gegeven de klasse "Bestelling" en onderstaande lijst producten
        List<Product> producten = new List<Product>
        {
            new Product { ProductId = 9112, Naam = "Laptop", Beschrijving = "14-inch, 16GB RAM", Prijs = 999.99m, Voorraad = 12 },
            new Product { ProductId = 2876, Naam = "Bureaulamp", Beschrijving = "LED, dimbaar", Prijs = 34.50m, Voorraad = 0 },
            new Product { ProductId = 3033, Naam = "Rugzak", Beschrijving = "Waterbestendig, 30L", Prijs = 59.95m, Voorraad = 8 },
            new Product { ProductId = 4441, Naam = "Koptelefoon", Beschrijving = "Noise-cancelling", Prijs = 149.00m, Voorraad = 3 },
            new Product { ProductId = 5508, Naam = "Muis", Beschrijving = "Draadloos, ergonomisch", Prijs = 29.99m, Voorraad = 20 },
            new Product { ProductId = 6274, Naam = "Toetsenbord", Beschrijving = "Mechanisch, RGB", Prijs = 89.95m, Voorraad = 7 },
            new Product { ProductId = 7390, Naam = "Webcam", Beschrijving = "Full HD, 1080p", Prijs = 64.50m, Voorraad = 5 },
            new Product { ProductId = 8115, Naam = "USB-hub", Beschrijving = "7 poorten, USB-C", Prijs = 24.99m, Voorraad = 0 },
            new Product { ProductId = 8823, Naam = "Monitor", Beschrijving = "27-inch, 4K IPS", Prijs = 449.00m, Voorraad = 4 },
            new Product { ProductId = 9647, Naam = "Telefoonhouder", Beschrijving = "Verstelbaar, bureaumodel", Prijs = 14.75m, Voorraad = 15 },
        };

        // 1. voeg als property aan Bestelling een List van "Product"-objecten toe (compositie: gebruik van een class in een andere class)

        // 2. maak twee bestellingen aan:
        //   - bestelling1: id = 1, klantnaam = "Amara Diallo", producten = laptop, rugzak en webcam
        //   - bestelling2: id = 2, klantnaam = "Yuna Kim", producten = laptop en monitor
        //   -> tip: maak een Bestelling aan, en voeg dan producten toe met Add(), bv. bestelling1.Producten.Add(producten[0]) om de laptop toe te voegen
        // ...

        Bestelling bestelling1 = new Bestelling
        {
            BestellingId = 1,
            KlantNaam = "Amara Diallo",
        };
        bestelling1.Producten.Add(producten[0]); // laptop
        bestelling1.Producten.Add(producten[2]); // rugzak
        bestelling1.Producten.Add(producten[6]); // webcam

        Bestelling bestelling2 = new Bestelling
        {
            BestellingId = 2,
            KlantNaam = "Yuna Kim",
        };
        bestelling2.Producten.Add(producten[0]); // laptop
        bestelling2.Producten.Add(producten[8]); // monitor

        // 3. implementeer de Bestelling.TotaalBedrag property en pas de ToString() methode aan zodat het totaalbedrag ook getoond wordt
        // test met onderstaande code of het totaalbedrag correct berekend wordt (haal uit commentaar):
        
        Console.WriteLine($"Totaalbedrag bestelling 1: {bestelling1.TotaalBedrag}");
        

        // 4. toon de details van de eerste bestelling, en van alle producten die erin zitten
        // ...
        Console.WriteLine($"\nDetails bestelling 1: {bestelling1.ToString()}");

        foreach (var product in bestelling1.Producten)
        {
            Console.WriteLine(product.ToString());
        }

        // 4. geef 5% korting op alle producten in bestelling 2 (gebruik de Product.Korting property), en toon daarna de details
        // ...

        foreach (var product in bestelling2.Producten)
        {
            product.Korting = 5; // 5% korting
        }

        Console.WriteLine($"\nDetails bestelling 2: {bestelling2.ToString()}");

        foreach (var product in bestelling2.Producten)
        {
            Console.WriteLine(product.ToString());
        }

    }
}
