using WorkoutPlannerWebApp.Models;

namespace WorkoutPlannerWebApp.Services.Interfaces
{
    public interface IWorkoutProgramService
    {
        WorkoutProgram GetWorkoutProgram(int id);
        IEnumerable<WorkoutProgram> GetWorkoutProgramList(string searchString);
        IEnumerable<WorkoutProgram> GetPublishedWorkoutProgramList(string searchString);
        IEnumerable<WorkoutProgram> GetWorkoutProgramList(ApplicationUser user);
        Task<WorkoutProgram> AddWorkoutProgram(WorkoutProgram program);
        Task<WorkoutProgram> UpdateWorkoutProgram(WorkoutProgram program);
        Task<WorkoutProgram> RemoveWorkoutProgram(WorkoutProgram program);
    }
}
