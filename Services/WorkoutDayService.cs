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

        public WorkoutDay GetWorkoutDay(int phaseId)
        {
            return context.WorkoutDays
                .Include(d => d.WorkoutProgram)
                .Include(d => d.WorkoutPhase)
                .Include(d => d.CustomExercises)
                    .ThenInclude(e => e.Exercise)
                .FirstOrDefault(d => d.WorkoutPhase.Id == phaseId);
        }

        public IEnumerable<WorkoutDay> GetWorkoutDayFromProgramList(int programId)
        {
            return context.WorkoutDays
                .Include(d => d.WorkoutProgram)
                .Include(d => d.WorkoutPhase)
                .Include(d => d.CustomExercises)
                    .ThenInclude(e => e.Exercise)
                .Where(d => d.WorkoutProgram.Id == programId)
                .ToList();
        }

        public IEnumerable<WorkoutDay> GetWorkoutDayFromPhaseList(int phaseId)
        {
            return context.WorkoutDays
                .Include(d => d.WorkoutProgram)
                .Include(d => d.WorkoutPhase)
                .Include(d => d.CustomExercises)
                    .ThenInclude(e => e.Exercise)
                .Where(d => d.WorkoutPhase.Id == phaseId)
                .ToList();
        }
        public async Task<WorkoutDay> AddWorkoutDay(WorkoutDay day)
        {
            context.WorkoutDays.Add(day);
            await context.SaveChangesAsync();
            return day;
        }

        public WorkoutDay UpdateWorkoutDay(WorkoutDay day)
        {
            context.WorkoutDays.Update(day);
            context.SaveChangesAsync();
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
