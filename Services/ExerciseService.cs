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
                .OrderByDescending(e => e.Name)
                .FirstOrDefault(e => e.Id == id);
        }

        public CustomExercise GetCustomExercise(int id, ModelType modelType)
        {
            if (modelType == ModelType.WorkoutProgram)
            {
                return context.CustomExercises
                    .Include(e => e.WorkoutProgram)
                    .Include(e => e.WorkoutPhase)
                    .Include(e => e.WorkoutDay)
                    .Include(e => e.Exercise)
                    .FirstOrDefault(e => e.WorkoutProgram.Id == id);
            }
            else if (modelType == ModelType.WorkoutPhase)
            {
                return context.CustomExercises
                    .Include(e => e.WorkoutProgram)
                    .Include(e => e.WorkoutPhase)
                    .Include(e => e.WorkoutDay)
                    .Include(e => e.Exercise)
                    .FirstOrDefault(e => e.WorkoutPhase.Id == id);
            }
            else if (modelType == ModelType.WorkoutDay)
            {
                return context.CustomExercises
                    .Include(e => e.WorkoutProgram)
                    .Include(e => e.WorkoutPhase)
                    .Include(e => e.WorkoutDay)
                    .Include(e => e.Exercise)
                    .FirstOrDefault(e => e.WorkoutDay.Id == id);
            }
            else
            {
                return context.CustomExercises
                    .Include(e => e.WorkoutProgram)
                    .Include(e => e.WorkoutPhase)
                    .Include(e => e.WorkoutDay)
                    .Include(e => e.Exercise)
                    .FirstOrDefault(e => e.Id == id);
            }
        }

        public IEnumerable<Exercise> GetExerciseList(string searchString)
        {
            return context.Exercises
                .Where(e => e.Name.Contains(searchString ?? String.Empty))
                .OrderBy(e => e.Name)
                .ToList();
        }

        public IEnumerable<CustomExercise> GetCustomExerciseList(int id, ModelType modelType)
        {
            if (modelType == ModelType.WorkoutProgram)
            {
                return context.CustomExercises
                    .Include(e => e.WorkoutProgram)
                    .Include(e => e.WorkoutPhase)
                    .Include(e => e.WorkoutDay)
                    .Include(e => e.Exercise)
                    .Where(e => e.WorkoutProgram.Id == id)
                    .ToList();
            }
            else if (modelType == ModelType.WorkoutPhase)
            {
                return context.CustomExercises
                    .Include(e => e.WorkoutProgram)
                    .Include(e => e.WorkoutPhase)
                    .Include(e => e.WorkoutDay)
                    .Include(e => e.Exercise)
                    .Where(e => e.WorkoutPhase.Id == id)
                    .ToList();
            }
            else if (modelType == ModelType.WorkoutDay)
            {
                return context.CustomExercises
                    .Include(e => e.WorkoutProgram)
                    .Include(e => e.WorkoutPhase)
                    .Include(e => e.WorkoutDay)
                    .Include(e => e.Exercise)
                    .Where(e => e.WorkoutDay.Id == id)
                    .ToList();
            }
            else
            {
                return context.CustomExercises
                    .Include(e => e.WorkoutProgram)
                    .Include(e => e.WorkoutPhase)
                    .Include(e => e.WorkoutDay)
                    .Include(e => e.Exercise)
                    .Where(e => e.Id == id)
                    .ToList();
            }
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
