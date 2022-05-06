using Microsoft.EntityFrameworkCore;
using WorkoutPlannerWebApp.Data;
using WorkoutPlannerWebApp.Models;
using WorkoutPlannerWebApp.Services.Interfaces;

namespace WorkoutPlannerWebApp.Services
{
    public class WorkoutDayService : IWorkoutDayService
    {
        private readonly ApplicationDbContext context;

        public WorkoutDayService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public WorkoutDay GetWorkoutDay(int id, ModelType modelType)
        {
            if (modelType.Equals(ModelType.WorkoutProgram))
            {
                return context.WorkoutDays
                    .Include(d => d.WorkoutProgram)
                    .Include(d => d.WorkoutPhase)
                    .Include(d => d.CustomExercises)
                        .ThenInclude(e => e.Exercise)
                    .FirstOrDefault(d => d.WorkoutProgram.Id == id);
            }
            else if (modelType.Equals(ModelType.WorkoutPhase))
            {
                return context.WorkoutDays
                    .Include(d => d.WorkoutProgram)
                    .Include(d => d.WorkoutPhase)
                    .Include(d => d.CustomExercises)
                        .ThenInclude(e => e.Exercise)
                    .FirstOrDefault(d => d.WorkoutPhase.Id == id);
            }
            else
            {
                return context.WorkoutDays
                    .Include(d => d.WorkoutProgram)
                    .Include(d => d.WorkoutPhase)
                    .Include(d => d.CustomExercises)
                        .ThenInclude(e => e.Exercise)
                    .FirstOrDefault(d => d.Id == id);
            }
        }

        public IEnumerable<WorkoutDay> GetWorkoutDayList(int programId, ModelType modelType = ModelType.WorkoutProgram)
        {
            if (modelType.Equals(ModelType.WorkoutProgram))
            {
                return context.WorkoutDays
                    .Include(d => d.WorkoutProgram)
                    .Include(d => d.WorkoutPhase)
                    .Include(d => d.CustomExercises)
                        .ThenInclude(e => e.Exercise)
                    .Where(d => d.WorkoutProgram.Id == programId)
                    .ToList();
            }
            else if (modelType.Equals(ModelType.WorkoutPhase))
            {
                return context.WorkoutDays
                    .Include(d => d.WorkoutProgram)
                    .Include(d => d.WorkoutPhase)
                    .Include(d => d.CustomExercises)
                        .ThenInclude(e => e.Exercise)
                    .Where(d => d.WorkoutPhase.Id == programId)
                    .ToList();
            }
            else 
            {
                return context.WorkoutDays
                    .Include(d => d.WorkoutProgram)
                    .Include(d => d.WorkoutPhase)
                    .Include(d => d.CustomExercises)
                        .ThenInclude(e => e.Exercise)
                    .Where(d => d.Id == programId)
                    .ToList();
            }
        }

        public async Task<WorkoutDay> AddWorkoutDay(WorkoutDay day)
        {
            context.WorkoutDays.Add(day);
            await context.SaveChangesAsync();
            return day;
        }

        public async Task<WorkoutDay> UpdateWorkoutDay(WorkoutDay day)
        {
            context.WorkoutDays.Update(day);
            await context.SaveChangesAsync();
            return day;
        }

        public async Task<WorkoutDay> UpdateWorkoutDaySync(WorkoutDay day)
        {
            context.WorkoutDays.Update(day);
            await context.SaveChangesAsync();
            return day;
        }

        public async Task<WorkoutDay> RemoveWorkoutDay(WorkoutDay day)
        {
            context.WorkoutDays.Remove(day);
            await context.SaveChangesAsync();
            return day;
        }
    }
}
