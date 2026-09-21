using backend;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data source=coachedbyscience.db"));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowFrontend");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!db.Exercises.Any())
    {
        var biceps = new MuscleGroup(0, "Biceps");
        var chest = new MuscleGroup(0, "Chest");
        var quadriceps = new MuscleGroup(0, "Quadriceps");
        var glutes = new MuscleGroup(0, "Glutes");
        var hamstrings = new MuscleGroup(0, "Hamstrings");

        var preacherCurls = new Exercise(0, "Preacher Curls", "Preacher curls are a strict isolation exercise for the biceps performed by resting the upper arms on an angled pad while lifting a weight like an EZ-bar or dumbbell.");
        preacherCurls.MuscleGroups.Add(biceps);

        var chestPress = new Exercise(0, "Converging Machine Chest Press", "A converging chest press machine is a strength training device featuring handles that move forward and inward together during a press. This arc-like motion mimics natural human biomechanics, maximizing chest muscle contraction while reducing shoulder stress.");
        chestPress.MuscleGroups.Add(chest);

        var pendulumSquat = new Exercise(0, "Pendulum Squat", "The pendulum squat is a guided, machine-based lower-body exercise where your body moves along a curved, swinging arc instead of a straight path. It isolates the quadriceps, glutes, and hamstrings while offering deep range of motion with minimal axial spine load.");
        pendulumSquat.MuscleGroups.Add(quadriceps);
        pendulumSquat.MuscleGroups.Add(glutes);
        pendulumSquat.MuscleGroups.Add(hamstrings);

        db.Exercises.AddRange(preacherCurls, chestPress, pendulumSquat);
        db.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/api/exercises", (AppDbContext db) =>
{
    var exercises = db.Exercises
        .Include(e => e.MuscleGroups)
        .Select(e => new ExerciseResponse(
            e.Id,
            e.Name,
            e.Description,
            e.MuscleGroups.Select(mg => new MuscleGroupSummary(mg.Id, mg.Name)).ToList()
        ))
        .ToList();

    return exercises;
})
.WithName("GetExercises");

app.MapGet("/api/musclegroups", (AppDbContext db) =>
{
    var muscleGroups = db.MuscleGroups
        .Include(mg => mg.Exercises)
        .Select(mg => new MuscleGroupResponse(
            mg.Id,
            mg.Name,
            mg.Exercises.Select(e => new ExerciseSummary(e.Id, e.Name, e.Description)).ToList()
        ))
        .ToList();

    return muscleGroups;
})
.WithName("GetMuscleGroups");

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