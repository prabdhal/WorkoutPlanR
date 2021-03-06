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
                            p.Description.Contains(searchString))
                .OrderByDescending(p => p.UpdatedOn)
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
                            p.Description.Contains(searchString))
                .Where(p => p.Published)
                .OrderByDescending(p => p.UpdatedOn)
                .ToList();
        }

        public async Task<IEnumerable<WorkoutProgram>> GetWorkoutProgramList(ApplicationUser user)
        {
            return await context.WorkoutPrograms
                .Include(p => p.WorkoutPhases)
                    .ThenInclude(ph => ph.WorkoutDays)
                        .ThenInclude(e => e.CustomExercises)
                .Include(p => p.Publisher)
                .Where(p => p.Publisher == user)
                .OrderByDescending(p => p.UpdatedOn)
                .ToListAsync();
        }

        public async Task<WorkoutProgram> AddWorkoutProgram(WorkoutProgram program)
        {
            context.WorkoutPrograms.Add(program);
            await context.SaveChangesAsync();
            return program;
        }

        public async Task<WorkoutProgram> UpdateWorkoutProgram(WorkoutProgram program)
        {
            context.WorkoutPrograms.Update(program);
            await context.SaveChangesAsync();
            return program;
        }

        public async Task<WorkoutProgram> UpdateWorkoutProgramSync(WorkoutProgram program)
        {
            context.WorkoutPrograms.Update(program);
            await context.SaveChangesAsync();
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
