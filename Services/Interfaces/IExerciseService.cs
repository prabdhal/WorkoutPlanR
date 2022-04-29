using WorkoutPlannerWebApp.Models;

namespace WorkoutPlannerWebApp.Services.Interfaces
{
    public interface IExerciseService
    {
        Exercise GetExercise(int id);
        CustomExercise GetCustomExercise(int dayId);
        IEnumerable<Exercise> GetExerciseList();
        IEnumerable<CustomExercise> GetCustomExerciseFromProgramList(int programId);
        IEnumerable<CustomExercise> GetCustomExerciseFromPhaseList(int programId);
        IEnumerable<CustomExercise> GetCustomExerciseFromDayList(int dayId);
        Task<CustomExercise> AddCustomExercise(CustomExercise exercise);
        CustomExercise UpdateCustomExercise(CustomExercise exercise);
        Task<CustomExercise> RemoveCustomExercise(CustomExercise exercise);
    }
}
