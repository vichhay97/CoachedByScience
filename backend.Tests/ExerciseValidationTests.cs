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
}
