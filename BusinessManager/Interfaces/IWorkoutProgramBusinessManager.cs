using WorkoutPlannerWebApp.ViewModels;

namespace WorkoutPlannerWebApp.BusinessManager.Interfaces
{
    public interface IWorkoutProgramBusinessManager
    {
        IndexWorkoutProgramViewModel GetIndexWorkoutProgramsViewModel(string searchString);
        DetailWorkoutProgramViewModel GetDetailWorkoutProgramsViewModel(int id);
    }
}
