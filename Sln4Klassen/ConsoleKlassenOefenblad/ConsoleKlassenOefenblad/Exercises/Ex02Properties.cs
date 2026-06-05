using ConsoleKlassenOefenblad.Exercises.Classes;

namespace ConsoleKlassenOefenblad.Exercises;

internal class Ex02Properties
{
    public static void Run()
    {
        Console.WriteLine("\nOefening 2: properties, standaardwaarden, object initializer syntax");
        Console.WriteLine("-------------");
        // 1. maak in "Exercises/Classes" een klasse "Recept" met volgende properties:
        //   - Titel
        //   - Rating (int)
        //   - IsVegetarisch (standaardwaarde is false)
        //   - Ingredienten (List van strings, standaard lege lijst)

        // 2. maak volgend recept aan met de lege constructor (... = new Recept()) en stel dan één voor één de properties in:
        //   - Pasta Carbonara (Rating 4, IsVegetarisch true, Ingrediënten: Pasta, Eieren, Spek, Parmezaanse kaas)
        // ...

        Recept PastaCarbonara = new Recept();
        PastaCarbonara.Titel = "Pasta Carbonara";
        PastaCarbonara.Rating = 4;
        PastaCarbonara.IsVegetarisch = true;
        PastaCarbonara.Ingredienten = new List<string> { "Pasta", "Eieren", "Spek", "Parmezaanse kaas" };

        // 3. maak volgende recepten aan met de object initializer syntax:
        //   - Lasagne (Rating 5, IsVegetarisch false, Ingrediënten: Lasagnebladen, Tomatensaus, Courgette, Aubergine, Mozzarella)
        //   - Salade Niçoise (Rating 4, IsVegetarisch true, Ingrediënten: Sla, Tonijn, Eieren, Pindakaas, Olijven, Tomaten)
        // ...

        Recept Lasagne = new Recept
        {
            Titel = "Lasagne",
            Rating = 5,
            IsVegetarisch = false,
            Ingredienten = new List<string> {"Lasagnebladen", "Tomatensaus", "Courgette", "Aubergine", "Mozzarella" }
        };

        Recept SaladeNicoise = new Recept
        {
            Titel = "Salade Niçoise",
            Rating = 4,
            IsVegetarisch = true,
            Ingredienten = new List<string> { "Sla", "Tonijn", "Eieren", "Pindakaas", "Olijven", "Tomaten" }
        };

        // 4. pas het recept van de salade niçoise aan:
        //  - verwijder de pindakaas
        //  - zet IsVegetarisch op false
        // ...

        SaladeNicoise.Ingredienten.Remove("Pindakaas");
        SaladeNicoise.IsVegetarisch = false;

        // 5. maak een lijst "kookboek" aan en voeg de drie recepten toe
        // ...

        List<Recept> kookboek = new List<Recept>() {PastaCarbonara, Lasagne, SaladeNicoise };

        // 6. toon het aantal vegetarische recepten (zie screenshot) en de gemiddelde rating
        // ...

        int vegetarischeRecepten = 0;

        foreach (Recept recept in kookboek)
        {
            if (recept.IsVegetarisch)
            {
                vegetarischeRecepten++;
            }
        }

        double gemiddeldeRating = (PastaCarbonara.Rating + Lasagne.Rating + SaladeNicoise.Rating) / 3.0;



        Console.WriteLine($"Aantal vegetarische recepten in het kookboek: {vegetarischeRecepten}");
        Console.WriteLine($"De gemiddelde rating is: {gemiddeldeRating:F1}");

    }
}
