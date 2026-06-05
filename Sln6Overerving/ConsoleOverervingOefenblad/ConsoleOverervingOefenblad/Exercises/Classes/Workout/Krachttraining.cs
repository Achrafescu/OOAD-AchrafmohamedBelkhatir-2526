namespace ConsoleOverervingOefenblad.Exercises.Classes.Workout;

internal class Krachttraining : Workout
{
    public double Gewicht { get; set; }
    public int Reps { get; set; }

    public double Punten
    {
        get
        {
            return ((Gewicht * Reps) / 5);
        }
    }
}
