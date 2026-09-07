namespace backend;

public record MuscleGroup(int Id, string Name)
{
    public List<Exercise> Exercises { get; set; } = [];
}