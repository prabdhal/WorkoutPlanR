using Microsoft.AspNetCore.Mvc;
using WorkoutPlannerWebApp.Models;
using WorkoutPlannerWebApp.ViewModels;
using WorkoutPlannerWebApp.ViewModels.MyWorkoutProgramsViewModels;

namespace WorkoutPlannerWebApp.BusinessManager.Interfaces
{
    public interface IMyWorkoutPhaseBusinessManager
    {
        CreateWorkoutPhaseViewModel GetCreateWorkoutPhaseViewModel(int programId);
        EditWorkoutDayViewModel GetEditWorkoutDayViewModel(int programId);
        EditWorkoutPhaseViewModel GetEditWorkoutPhaseViewModel(int programId);
        ViewWorkoutPhaseViewModel GetViewWorkoutPhasesViewModel(int programId, int? dayId);
        WorkoutPhase GetWorkoutPhase(int id, ModelType modelType);
        WorkoutDay GetWorkoutDay(int id, ModelType modelType);
        Task<ActionResult<WorkoutDay>> EditWorkoutDay(CreateExerciseViewModel editViewModel);
        Task<WorkoutPhase> CreateWorkoutPhase(CreateWorkoutPhaseViewModel createViewModel);
        ActionResult<WorkoutPhase> EditWorkoutPhase(EditWorkoutPhaseViewModel editViewModel);
        Task<WorkoutPhase> DeleteWorkoutPhase(int id);
        Task<ActionResult<WorkoutDay>> ClearWorkoutDay(int dayId);
        Task<WorkoutDay> PasteWorkoutDay(int copyDayId, int pasteDayId);
    }
}
