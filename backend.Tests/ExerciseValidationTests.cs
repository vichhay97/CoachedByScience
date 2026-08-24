namespace CoachedByScience.Tests;

using Xunit;
using backend;

public class ExerciseValidationTests
{
    [Fact]
    public void IsValid_ReturnsFalse_WhenNameIsEmpty()
    {
        var request = new CreateExerciseRequest("", "Some description");

        bool result = ExerciseValidator.IsValid(request);

        Assert.False(result);
    }

    [Fact]
    public void IsValid_ReturnsTrue_WhenNameAndDescriptionIsNotEmpty()
    {
        var request = new CreateExerciseRequest("Name", "Some description");

        bool result = ExerciseValidator.IsValid(request);

        Assert.True(result);
    }

    [Fact]
    public void IsValid_ReturnsFalse_WhenDescriptionIsEmpty()
    {
        var request = new CreateExerciseRequest("Name", "");

        bool result = ExerciseValidator.IsValid(request);

        Assert.False(result);
    }
}
