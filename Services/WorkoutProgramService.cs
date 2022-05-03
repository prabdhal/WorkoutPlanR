using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkoutPlannerWebApp.Data;
using WorkoutPlannerWebApp.Models;
using WorkoutPlannerWebApp.Services.Interfaces;

namespace WorkoutPlannerWebApp.Services
{
    public class WorkoutProgramService : IWorkoutProgramService
    {
        private readonly ApplicationDbContext context;


        public WorkoutProgramService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public WorkoutProgram GetWorkoutProgram(int programId)
        {

             return context.WorkoutPrograms
                .Include(p => p.WorkoutPhases)
                    .ThenInclude(ph => ph.WorkoutDays)
                        .ThenInclude(e => e.CustomExercises)
                            .ThenInclude(e => e.Exercise)
                .Include(p => p.Publisher)
                .FirstOrDefault(p => p.Id == programId);
        }

        public IEnumerable<WorkoutProgram> GetWorkoutProgramList(string searchString)
        {
            return context.WorkoutPrograms
                .Include(p => p.WorkoutPhases)
                    .ThenInclude(ph => ph.WorkoutDays)
                        .ThenInclude(e => e.CustomExercises)
                .Include(p => p.Publisher)
                .Where(p => p.Name.Contains(searchString) ||
                            p.Publisher.FirstName.Contains(searchString) ||
                            p.Publisher.LastName.Contains(searchString) ||
                            p.ShortDescription.Contains(searchString))
                .ToList();
        }

        public IEnumerable<WorkoutProgram> GetPublishedWorkoutProgramList(string searchString)
        {
            return context.WorkoutPrograms
                .Include(p => p.WorkoutPhases)
                    .ThenInclude(ph => ph.WorkoutDays)
                        .ThenInclude(e => e.CustomExercises)
                .Include(p => p.Publisher)
                .Where(p => p.Name.Contains(searchString) ||
                            p.Publisher.FirstName.Contains(searchString) ||
                            p.Publisher.LastName.Contains(searchString) ||
                            p.ShortDescription.Contains(searchString))
                .Where(p => p.Published)
                .ToList();
        }

        public IEnumerable<WorkoutProgram> GetWorkoutProgramList(ApplicationUser user)
        {
            return context.WorkoutPrograms
                .Include(p => p.WorkoutPhases)
                    .ThenInclude(ph => ph.WorkoutDays)
                        .ThenInclude(e => e.CustomExercises)
                .Include(p => p.Publisher)
                .Where(p => p.Publisher == user)
                .ToList();
        }

        public async Task<WorkoutProgram> AddWorkoutProgram(WorkoutProgram program)
        {
            context.WorkoutPrograms.Add(program);   
            await context.SaveChangesAsync();
            return program;
        }

        public WorkoutProgram UpdateWorkoutProgram(WorkoutProgram program)
        {
            context.WorkoutPrograms.Update(program);
            context.SaveChanges();
            return program;
        }

        public async Task<WorkoutProgram> RemoveWorkoutProgram(WorkoutProgram program)
        {
            context.WorkoutPrograms.Remove(program);
            await context.SaveChangesAsync();
            return program;
        }
    }
}
