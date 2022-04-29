using WorkoutPlannerWebApp.Models;
using WorkoutPlannerWebApp.ViewModels;

namespace WorkoutPlannerWebApp.BusinessManager.Interfaces
{
    public interface IExerciseBusinessManager
    {
        CustomExercise GetCustomExercise(int exerciseId);
        IEnumerable<Exercise> GetExercises();
        Task<CustomExercise> CreateCustomExercise(CreateExerciseMyWorkoutProgramViewModel createViewModel);
        Task<CustomExercise> DeleteCustomExercise(int id);
        CreateExerciseMyWorkoutProgramViewModel GetCreateExerciseMyWorkoutProgramsViewModel(int programId, out IEnumerable<Exercise> exercises);
    }
}
