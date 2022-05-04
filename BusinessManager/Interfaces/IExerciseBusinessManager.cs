using WorkoutPlannerWebApp.Models;
using WorkoutPlannerWebApp.ViewModels;
using WorkoutPlannerWebApp.ViewModels.ExercisesViewModels;

namespace WorkoutPlannerWebApp.BusinessManager.Interfaces
{
    public interface IExerciseBusinessManager
    {
        IndexExercisesViewModel GetIndexExercisesViewModel();
        DetailExerciseViewModel GetDetailExerciseViewModel(int exerciseId);
        CreateExerciseViewModel GetCreateExerciseMyWorkoutProgramsViewModel(int programId, ModelType modelType, out IEnumerable<Exercise> exercises);
        CustomExercise GetCustomExercise(int exerciseId);
        IEnumerable<Exercise> GetExercises();
        Task<CustomExercise> CreateCustomExercise(CreateExerciseViewModel createViewModel);
        Task<CustomExercise> DeleteCustomExercise(int id, ModelType modelType);
    }
}
