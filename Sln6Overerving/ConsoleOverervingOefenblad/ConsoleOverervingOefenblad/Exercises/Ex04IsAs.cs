using ConsoleOverervingOefenblad.Exercises.Classes.Workout;

namespace ConsoleOverervingOefenblad.Exercises;

/// <summary>
/// Oefening 4 — is en as: type controle en veilige cast bij overerving
/// </summary>
internal static class Ex04IsAs
{
    public static void Run()
    {
        Console.WriteLine("Oefening 4: is en as");
        Console.WriteLine("-------------");

        List<Workout> workouts = new List<Workout>
        {
            new Cardio
            {
                Naam = "Ochtendrun",
                Beschrijving = "Rustig tempo door het park",
                AfstandInKm = 5.2
            },
            new Krachttraining
            {
                Naam = "Bench press",
                Beschrijving = "Borstspieren",
                Gewicht = 60,
                Reps = 12
            },
            new Stretching
            {
                Naam = "Rugstretching",
                Beschrijving = "Na het tillen",
                LichaamsDeel = LichaamsDeel.Rug
            },
            new Cardio
            {
                Naam = "Fietstocht",
                Beschrijving = "Intervaltraining",
                AfstandInKm = 22.0
            },
            new Krachttraining
            {
                Naam = "Squat",
                Beschrijving = "Beenspieren",
                Gewicht = 80,
                Reps = 8
            },
            new Stretching
            {
                Naam = "Nekrol",
                Beschrijving = "Ontspanning na beeldschermwerk",
                LichaamsDeel = LichaamsDeel.Nek
            },
        };

        // TODO 1: toon info per workout met is/as of pattern matching
        // ...
        foreach (Workout wrkt in workouts)
        {
            if (wrkt is Cardio crdio)
            {
                Console.WriteLine($"[Cardio]    {crdio.Naam} - {crdio.AfstandInKm} km");
            }
            if (wrkt is Krachttraining krchttrng)
            {
                Console.WriteLine($"[Kracht]    {krchttrng.Naam} - {krchttrng.Gewicht} kg x {krchttrng.Reps} reps");
            }
            if (wrkt is Stretching strtchng)
            {
                Console.WriteLine($"[Stretching] {strtchng.Naam} - {strtchng.LichaamsDeel}");
            }
        }

        // TODO 2: bereken en toon de totale punten per type (Cardio, Krachttraining, Stretching)
        // ...
        double somCrdio = 0;
        double somKrchttrng = 0;
        double somStrtchng = 0;
        foreach (Workout wrkt in workouts)
        {
            if (wrkt is Cardio crdio)
            {
                 somCrdio += crdio.Punten;
            }
            if (wrkt is Krachttraining krchttrng)
            {
                 somKrchttrng += krchttrng.Punten;
            }
            if (wrkt is Stretching strtchng)
            {
                somStrtchng += strtchng.Punten;
            }
        }
        Console.WriteLine($"\nTotale punten per type:");
        Console.WriteLine($"Cardio: {somCrdio}");
        Console.WriteLine($"Kracht: {somKrchttrng}");
        Console.WriteLine($"Stretching: {somStrtchng}");
    }
}
