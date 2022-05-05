using WorkoutPlannerWebApp.ViewModels;
using WorkoutPlannerWebApp.ViewModels.WorkoutProgramsViewModel;

namespace WorkoutPlannerWebApp.BusinessManager.Interfaces
{
    public interface IWorkoutProgramBusinessManager
    {
        IndexWorkoutProgramViewModel GetIndexWorkoutProgramsViewModel(string searchString);
        DetailWorkoutProgramViewModel GetDetailWorkoutProgramsViewModel(int id);
        PhaseDetailWorkoutProgramViewModel GetPhaseDetailWorkoutProgramsViewModel(int id);
        FullDetailWorkoutProgramViewModel GetFullDetailWorkoutProgramsViewModel(int id);
    }
}
