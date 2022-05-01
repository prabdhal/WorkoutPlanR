using WorkoutPlannerWebApp.Models;
using WorkoutPlannerWebApp.ViewModels;

namespace WorkoutPlannerWebApp.BusinessManager.Interfaces
{
    public interface IExerciseBusinessManager
    {
        CreateExerciseMyWorkoutProgramViewModel GetCreateExerciseMyWorkoutProgramsViewModel(int programId, ModelType modelType, out IEnumerable<Exercise> exercises);
        CustomExercise GetCustomExercise(int exerciseId);
        IEnumerable<Exercise> GetExercises();
        Task<CustomExercise> CreateCustomExercise(CreateExerciseMyWorkoutProgramViewModel createViewModel);
        Task<CustomExercise> DeleteCustomExercise(int id, ModelType modelType);
    }
}
