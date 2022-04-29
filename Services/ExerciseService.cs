using Microsoft.EntityFrameworkCore;
using WorkoutPlannerWebApp.Data;
using WorkoutPlannerWebApp.Models;
using WorkoutPlannerWebApp.Services.Interfaces;

namespace WorkoutPlannerWebApp.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly ApplicationDbContext context;

        public ExerciseService(ApplicationDbContext context)
        {
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
                .Include(e => e.WorkoutProgram)
                .Include(e => e.WorkoutPhase)
                .Include(e => e.WorkoutDay)
                .Include(e => e.Exercise)
                .FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Exercise> GetExerciseList()
        {
            return context.Exercises
                .ToList();
        }

        public IEnumerable<CustomExercise> GetCustomExerciseFromProgramList(int programId)
        {
            return context.CustomExercises
                .Include(e => e.WorkoutProgram)
                .Include(e => e.WorkoutPhase)
                .Include(e => e.WorkoutDay)
                .Include(e => e.Exercise)
                .Where(e => e.WorkoutProgram.Id == programId)
                .ToList();
        }

        public IEnumerable<CustomExercise> GetCustomExerciseFromPhaseList(int phaseId)
        {
            return context.CustomExercises
                .Include(e => e.WorkoutProgram)
                .Include(e => e.WorkoutPhase)
                .Include(e => e.WorkoutDay)
                .Include(e => e.Exercise)
                .Where(e => e.WorkoutPhase.Id == phaseId)
                .ToList();
        }

        public IEnumerable<CustomExercise> GetCustomExerciseFromDayList(int dayId)
        {
            return context.CustomExercises
                .Include(e => e.WorkoutProgram)
                .Include(e => e.WorkoutPhase)
                .Include(e => e.WorkoutDay)
                .Include(e => e.Exercise)
                .Where(e => e.WorkoutDay.Id == dayId)
                .ToList();
        }

        public async Task<CustomExercise> AddCustomExercise(CustomExercise exercise)
        {
            context.CustomExercises.Add(exercise);  
            await context.SaveChangesAsync();
            return exercise;
        }

        public CustomExercise UpdateCustomExercise(CustomExercise exercise)
        {
            context.CustomExercises.Update(exercise);
            context.SaveChanges();
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
