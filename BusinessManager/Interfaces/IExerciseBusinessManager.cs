using WorkoutPlannerWebApp.Models;
using WorkoutPlannerWebApp.ViewModels;
using WorkoutPlannerWebApp.ViewModels.ExercisesViewModels;

namespace WorkoutPlannerWebApp.BusinessManager.Interfaces
{
    public interface IExerciseBusinessManager
    {
        IndexExercisesViewModel GetIndexExercisesViewModel(string searchString, int page = 0);
        DetailExerciseViewModel GetDetailExerciseViewModel(int exerciseId);
        CreateExerciseViewModel GetCreateExerciseMyWorkoutProgramsViewModel(int programId, ModelType modelType, out IEnumerable<Exercise> exercises);
        CustomExercise GetCustomExercise(int exerciseId);
        IEnumerable<Exercise> GetExercises(string searchString);
        Task<CustomExercise> CreateCustomExercise(CreateExerciseViewModel createViewModel);
        Task<CustomExercise> DeleteCustomExercise(int id, ModelType modelType);
    }
}
