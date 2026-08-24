using backend;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data source=coachedbyscience.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!db.Exercises.Any())
    {
        db.Exercises.AddRange(
            new Exercise(0, "Preacher Curls", "Preacher curls are a strict isolation exercise for the biceps performed by resting the upper arms on an angled pad while lifting a weight like an EZ-bar or dumbbell."),
            new Exercise(0, "Converging Machine Chest Press", "A converging chest press machine is a strength training device featuring handles that move forward and inward together during a press. This arc-like motion mimics natural human biomechanics, maximizing chest muscle contraction while reducing shoulder stress."),
            new Exercise(0, "Pendulum Squat", "The pendulum squat is a guided, machine-based lower-body exercise where your body moves along a curved, swinging arc instead of a straight path. It isolates the quadriceps, glutes, and hamstrings while offering deep range of motion with minimal axial spine load.")

        );
        db.SaveChanges();
    }
}

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

app.MapGet("/api/exercises", (AppDbContext db) =>
{
    return db.Exercises.ToList();
})
.WithName("GetExercises");

app.MapPost("/api/exercises", (CreateExerciseRequest request, AppDbContext db) =>
{
    if (!ExerciseValidator.IsValid(request))
    {
        return Results.BadRequest("Name and description are required.");
    }

    var exercise = new Exercise(0, request.Name, request.Description);
    db.Exercises.Add(exercise);
    db.SaveChanges();
    return Results.Created($"/api/exercises/{exercise.Id}", exercise);
})
.WithName("CreateExercise");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
