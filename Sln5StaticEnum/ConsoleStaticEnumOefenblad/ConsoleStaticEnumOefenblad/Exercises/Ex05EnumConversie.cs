using ConsoleStaticEnumOefenblad.Exercises.Classes;

namespace ConsoleStaticEnumOefenblad.Exercises;

internal class Ex05EnumConversie
{
    public static void Run()
    {
        Console.WriteLine("\nOefening 5: enum conversies");
        Console.WriteLine("-------------");

        // 1. Maak in "Exercises/Classes" een enum "Prioriteit" met deze waarden:
        //  - Laag
        //  - Normaal
        //  - Hoog
        //  - Kritiek
        //
        //  Test daarna:
        //  - een enumwaarde tonen als tekst
        Prioriteit testEnumString = Prioriteit.Hoog;
        Console.WriteLine($"Enum als tekst: {testEnumString}");
        //  - dezelfde enumwaarde omzetten naar int
        int testEnumInt = (int)testEnumString;
        Console.WriteLine($"Enum als int: {testEnumInt}");
        //  - een int omzetten naar enum
        Prioriteit testIntEnum = (Prioriteit)testEnumInt;
        Console.WriteLine($"Int als enum: {testIntEnum}");

        // Testcode (haal uit commentaar):

        Prioriteit p1 = Prioriteit.Hoog;
        Console.WriteLine($"Enumwaarde: {p1}");

        int cijfer = (int)p1;
        Console.WriteLine($"Als int: {cijfer}");

        Prioriteit p2 = p1 + 1;
        Console.WriteLine($"Nog hogere prioriteit: {p2}");
    }
}