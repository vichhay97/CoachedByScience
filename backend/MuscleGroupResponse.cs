namespace backend;

public record ExerciseSummary(int Id, string Name, string Description);

public record MuscleGroupResponse(int Id, string Name, List<ExerciseSummary> Exercises);