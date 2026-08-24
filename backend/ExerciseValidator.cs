namespace backend;

public static class ExerciseValidator
{
    public static bool IsValid(CreateExerciseRequest request)
    {
        return !string.IsNullOrWhiteSpace(request.Name) && !string.IsNullOrWhiteSpace(request.Description);
    }
}