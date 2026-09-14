namespace backend;

public record MuscleGroupSummary(int Id, string Name);

public record ExerciseResponse(int Id, string Name, string Description, List<MuscleGroupSummary> MuscleGroups);