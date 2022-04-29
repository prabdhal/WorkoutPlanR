using WorkoutPlannerWebApp.Models;

namespace WorkoutPlannerWebApp.Services.Interfaces
{
    public interface IWorkoutPhaseService
    {
        WorkoutPhase GetWorkoutPhase(int programId);
        IEnumerable<WorkoutPhase> GetWorkoutPhaseList(int programId);
        Task<WorkoutPhase> AddWorkoutPhase(WorkoutPhase phase);
        WorkoutPhase UpdateWorkoutPhase(WorkoutPhase phase);
        Task<WorkoutPhase> RemoveWorkoutPhase(WorkoutPhase phase);
    }
}
