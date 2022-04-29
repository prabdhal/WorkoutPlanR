#nullable disable
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkoutPlannerWebApp.Models;

namespace WorkoutPlannerWebApp.Data
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<WorkoutProgram> WorkoutPrograms { get; set; }
    public DbSet<WorkoutPhase> WorkoutPhases { get; set; }
    public DbSet<WorkoutDay> WorkoutDays { get; set; }
    public DbSet<CustomExercise> CustomExercises { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
  }
}