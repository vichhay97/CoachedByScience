var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/api/exercises", () =>
{
    List<Exercise> exercises = [
    new Exercise(1, "Preacher Curls", "Preacher curls are a strict isolation exercise for the biceps performed by resting the upper arms on an angled pad while lifting a weight like an EZ-bar or dumbbell."),

    new Exercise(2, "Converging Machine Chest Press", "A converging chest press machine is a strength training device featuring handles that move forward and inward together during a press. This arc-like motion mimics natural human biomechanics, maximizing chest muscle contraction while reducing shoulder stress."),

    new Exercise(3, "Pendulum Squat", "The pendulum squat is a guided, machine-based lower-body exercise where your body moves along a curved, swinging arc instead of a straight path. It isolates the quadriceps, glutes, and hamstrings while offering deep range of motion with minimal axial spine load.")
    ];

    return exercises;
})
.WithName("GetExercises");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
