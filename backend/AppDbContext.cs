namespace backend;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Exercise> Exercises { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}