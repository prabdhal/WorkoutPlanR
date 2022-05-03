using WorkoutPlannerWebApp.Models;

namespace WorkoutPlannerWebApp.Services.Interfaces
{
    public interface IWorkoutPhaseService
    {
        WorkoutPhase GetWorkoutPhase(int id, ModelType modelType);
        IEnumerable<WorkoutPhase> GetWorkoutPhaseList(int id, ModelType modelType);
        Task<WorkoutPhase> AddWorkoutPhase(WorkoutPhase phase);
        Task<WorkoutPhase> UpdateWorkoutPhase(WorkoutPhase phase);
        WorkoutPhase UpdateWorkoutPhaseSync(WorkoutPhase phase);
        Task<WorkoutPhase> RemoveWorkoutPhase(WorkoutPhase phase);
    }
}
