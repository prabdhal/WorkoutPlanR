using Microsoft.AspNetCore.Mvc;
using WorkoutPlannerWebApp.Models;
using WorkoutPlannerWebApp.ViewModels;
using WorkoutPlannerWebApp.ViewModels.MyWorkoutProgramsViewModels;

namespace WorkoutPlannerWebApp.BusinessManager.Interfaces
{
    public interface IMyWorkoutPhaseBusinessManager
    {
        CreateWorkoutPhaseMyWorkoutProgramViewModel GetCreateWorkoutPhaseMyWorkoutProgramViewModel(int programId);
        WorkoutPhase GetWorkoutPhase(int id, ModelType modelType);
        WorkoutDay GetWorkoutDay(int id, ModelType modelType);
        Task<WorkoutPhase> CreateWorkoutPhase(CreateWorkoutPhaseMyWorkoutProgramViewModel createViewModel);
        ActionResult<WorkoutDay> EditWorkoutDay(CreateExerciseMyWorkoutProgramViewModel editViewModel);
        Task<WorkoutPhase> DeleteWorkoutPhase(int id);
        Task<ActionResult<WorkoutDay>> ClearWorkoutDay(int dayId);
    }
}
