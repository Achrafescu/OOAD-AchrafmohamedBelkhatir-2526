namespace ConsoleKlassenOefenblad.Exercises.Classes;

internal class Bestelling
{
    // Properties
    public int BestellingId { get; set; }
    public DateTime Datum { get; set; } = DateTime.Now;
    public string KlantNaam { get; set; }
    public string Status
    {
        get;
        set
        {
            string[] toegelaten = { "Bezig", "Afgerond", "Geannuleerd" };
            if (!toegelaten.Contains(value)) throw new ArgumentException($"Ongeldige status: {value}");
            field = value;
        }
    } = "Bezig";

    public List<Product> Producten { get; set; } = new List<Product>();

    // Berekende properties
    public decimal TotaalBedrag 
    {
        get 
        {
            decimal som = 0;
            foreach (var product in Producten)
            {
                som += product.PrijsMetKorting;
            }
            return som;
            // verwijder deze regel en implementeer deze property
            // ...
        }
    }

    // ToString override
    public override string ToString()
    {
        // pas dit aan zodat het aantal producten weergegeven wordt
        return $"#{BestellingId} — {KlantNaam} | {Producten.Count} product(en) | € {TotaalBedrag:F2} | {Status}";
    }
}
