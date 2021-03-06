using WorkoutPlannerWebApp.Models;

namespace WorkoutPlannerWebApp.Services.Interfaces
{
    public interface IExerciseService
    {
        Exercise GetExercise(int id);
        CustomExercise GetCustomExercise(int id, ModelType modelType);
        IEnumerable<Exercise> GetExerciseList(string searchString);
        IEnumerable<CustomExercise> GetCustomExerciseList(int id, ModelType modelType);
        Task<CustomExercise> AddCustomExercise(CustomExercise exercise);
        Task<CustomExercise> UpdateCustomExercise(CustomExercise exercise);
        Task<CustomExercise> RemoveCustomExercise(CustomExercise exercise);
    }
}
