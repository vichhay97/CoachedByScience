namespace backend;

public record Exercise(int Id, string Name, string Description)
{
    public List<MuscleGroup> MuscleGroups { get; set; } = [];
}