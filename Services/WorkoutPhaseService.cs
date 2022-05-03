using Microsoft.EntityFrameworkCore;
using WorkoutPlannerWebApp.Data;
using WorkoutPlannerWebApp.Models;
using WorkoutPlannerWebApp.Services.Interfaces;

namespace WorkoutPlannerWebApp.Services
{
    public class WorkoutPhaseService : IWorkoutPhaseService
    {
        private readonly ApplicationDbContext context;

        public WorkoutPhaseService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public WorkoutPhase GetWorkoutPhase(int id, ModelType modelType)
        {
            if (modelType.Equals(ModelType.WorkoutProgram))
            {
                return context.WorkoutPhases
                    .Include(p => p.WorkoutProgram)
                    .Include(p => p.WorkoutDays)
                        .ThenInclude(d => d.CustomExercises)
                            .ThenInclude(e => e.Exercise)
                    .FirstOrDefault(p => p.WorkoutProgram.Id == id);
            }
            else
            {
                return context.WorkoutPhases
                    .Include(p => p.WorkoutProgram)
                    .Include(p => p.WorkoutDays)
                        .ThenInclude(d => d.CustomExercises)
                            .ThenInclude(e => e.Exercise)
                    .FirstOrDefault(p => p.Id == id);
            }
        }

        public IEnumerable<WorkoutPhase> GetWorkoutPhaseList(int programId, ModelType modelType)
        {
            if (modelType.Equals(ModelType.WorkoutProgram))
            {
                return context.WorkoutPhases
                    .Include(p => p.WorkoutProgram)
                    .Include(p => p.WorkoutDays)
                        .ThenInclude(d => d.CustomExercises)
                            .ThenInclude(e => e.Exercise)
                    .Where(p => p.WorkoutProgram.Id == programId)
                    .ToList();
            }
            else
            {
                return context.WorkoutPhases
                    .Include(p => p.WorkoutProgram)
                    .Include(p => p.WorkoutDays)
                        .ThenInclude(d => d.CustomExercises)
                            .ThenInclude(e => e.Exercise)
                    .Where(p => p.Id == programId)
                    .ToList();
            }
        }

        public async Task<WorkoutPhase> AddWorkoutPhase(WorkoutPhase phase)
        {
            context.WorkoutPhases.Add(phase);
            await context.SaveChangesAsync();
            return phase;
        }

        public async Task<WorkoutPhase> UpdateWorkoutPhase(WorkoutPhase phase)
        {
            context.WorkoutPhases.Update(phase);
            await context.SaveChangesAsync();
            return phase;
        }

        public WorkoutPhase UpdateWorkoutPhaseSync(WorkoutPhase phase)
        {
            context.WorkoutPhases.Update(phase);
            context.SaveChanges();
            return phase;
        }

        public async Task<WorkoutPhase> RemoveWorkoutPhase(WorkoutPhase phase)
        {
            context.WorkoutPhases.Remove(phase);
            await context.SaveChangesAsync();
            return phase;
        }
    }
}
