using WorkoutPlannerWebApp.Models;

namespace WorkoutPlannerWebApp.Services.Interfaces
{
    public interface IExerciseService
    {
        Exercise GetExercise(int id);
        CustomExercise GetCustomExercise(int id);
        IEnumerable<Exercise> GetExerciseList();
        IEnumerable<CustomExercise> GetCustomExerciseList(int programId);
        Task<CustomExercise> AddCustomExercise(CustomExercise exercise);
        Task<CustomExercise> UpdateCustomExercise(CustomExercise exercise);
        Task<CustomExercise> RemoveCustomExercise(CustomExercise exercise);
    }
}
