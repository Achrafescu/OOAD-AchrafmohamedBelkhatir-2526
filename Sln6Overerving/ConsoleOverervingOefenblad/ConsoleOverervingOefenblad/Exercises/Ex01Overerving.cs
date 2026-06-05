using ConsoleOverervingOefenblad.Exercises.Classes.Klant;

namespace ConsoleOverervingOefenblad.Exercises;

/// <summary>
/// Oefening 1 — Basisoefening op overerving: ProfessioneleKlant erft over van Klant
/// </summary>
internal static class Ex01Overerving
{
    public static void Run()
    {
        Console.WriteLine("Oefening 1: Overerving");
        Console.WriteLine("-------------");

        List<Klant> klanten = new List<Klant>();
        klanten.Add(new Klant { Naam = "Lotte Peeters", Email = "lotte.peeters@mail.be" });
        klanten.Add(new Klant { Naam = "Youssef El Amrani", Email = "youssef.elamrani@outlook.com" });
        klanten.Add(new Klant { Naam = "Chloé Van den Broeck", Email = "chloe.vdbroeck@gmail.com" });
        klanten.Add(new Klant { Naam = "Milan De Vos", Email = "milan.devos@yahoo.com" });
        // voeg twee instanties van ProfessioneleKlant toe
        // ...

        ProfessioneleKlant professioneleKlant1 = new ProfessioneleKlant
        {
            Naam = "Niels Verhoeven",
            Email = "niels@studio42.be",
            Bedrijfsnaam = "Studio 42",
            BtwNummer = "BE0123.456.789"
        };

        ProfessioneleKlant professioneleKlant2 = new ProfessioneleKlant
        {
            Naam = "Sofia Dimitris",
            Email = "sofia@nova.eu",
            Bedrijfsnaam = "Atelier Nova",
            BtwNummer = "BE0987.654.321"
        };

        klanten.Add(professioneleKlant1);
        klanten.Add(professioneleKlant2);

        Console.WriteLine("Overzicht klanten:");
        foreach (Klant klant in klanten)
        {
            Console.WriteLine($"- {klant}");
        }
    }
}
