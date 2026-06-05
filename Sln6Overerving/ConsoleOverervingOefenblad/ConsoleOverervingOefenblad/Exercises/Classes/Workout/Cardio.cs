namespace ConsoleOverervingOefenblad.Exercises.Classes.Workout;

internal class Cardio : Workout
{
    public double AfstandInKm { get; set; }

    public double Punten
    {
        get
        {
                return AfstandInKm * 6;
        }
    }
}
