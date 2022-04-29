using WorkoutPlannerWebApp.Models;
using WorkoutPlannerWebApp.ViewModels;
using WorkoutPlannerWebApp.ViewModels.MyWorkoutProgramsViewModels;

namespace WorkoutPlannerWebApp.BusinessManager.Interfaces
{
    public interface IMyWorkoutPhaseBusinessManager
    {
        WorkoutPhase GetWorkoutPhase(int phaseId);
        IEnumerable<WorkoutPhase> GetWorkoutPhases();
        Task<WorkoutPhase> CreateWorkoutPhase(CreateWorkoutPhaseMyWorkoutProgramViewModel createViewModel);
        Task<WorkoutPhase> DeleteWorkoutPhase(int id);
        CreateWorkoutPhaseMyWorkoutProgramViewModel GetCreateWorkoutPhaseMyWorkoutProgramViewModel(int programId);
    }
}
