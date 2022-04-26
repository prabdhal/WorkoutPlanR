﻿#nullable disable
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
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<ExerciseAPI> ExerciseAPIs { get; set; }
  }
}