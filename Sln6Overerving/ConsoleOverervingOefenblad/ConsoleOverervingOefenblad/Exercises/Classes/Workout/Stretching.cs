namespace ConsoleOverervingOefenblad.Exercises.Classes.Workout;

internal class Stretching : Workout
{
    public LichaamsDeel LichaamsDeel { get; set; }

    public double Punten
    {
        get
        {
            return 10;
        }
    }
}
