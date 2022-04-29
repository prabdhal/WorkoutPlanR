using Microsoft.EntityFrameworkCore;
using WorkoutPlannerWebApp.Data;
using WorkoutPlannerWebApp.Models;
using WorkoutPlannerWebApp.Services.Interfaces;

namespace WorkoutPlannerWebApp.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly ApplicationDbContext context;
        private readonly IWorkoutProgramService workoutProgramService;

        public ExerciseService(
            ApplicationDbContext context,
            IWorkoutProgramService workoutProgramService)
        {
            this.workoutProgramService = workoutProgramService;
            this.context = context;
        }

        public Exercise GetExercise(int id)
        {
            return context.Exercises
                .FirstOrDefault(e => e.Id == id);
        }

        public CustomExercise GetCustomExercise(int id)
        {
            return context.CustomExercises
                .Include(e => e.Exercise)
                .Include(e => e.WorkoutProgram)
                .FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Exercise> GetExerciseList()
        {
            return context.Exercises.ToList();
        }

        public IEnumerable<CustomExercise> GetCustomExerciseList(int programId)
        {
            return context.CustomExercises
                .Include(e => e.Exercise)
                .Include(e => e.WorkoutProgram)
                .Where(e => e.WorkoutProgram.Id == programId)
                .ToList();
        }

        public async Task<CustomExercise> AddCustomExercise(CustomExercise exercise)
        {
            context.CustomExercises.Add(exercise);  
            await context.SaveChangesAsync();
            return exercise;
        }

        public async Task<CustomExercise> UpdateCustomExercise(CustomExercise exercise)
        {
            context.CustomExercises.Update(exercise);
            await context.SaveChangesAsync();
            return exercise;
        }

        public async Task<CustomExercise> RemoveCustomExercise(CustomExercise exercise)
        {
            context.CustomExercises.Remove(exercise);
            await context.SaveChangesAsync();
            return exercise;
        }
    }
}
