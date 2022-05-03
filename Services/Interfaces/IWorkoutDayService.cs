using WorkoutPlannerWebApp.Models;

namespace WorkoutPlannerWebApp.Services.Interfaces
{
    public interface IWorkoutDayService
    {
        WorkoutDay GetWorkoutDay(int id, ModelType modelType);
        IEnumerable<WorkoutDay> GetWorkoutDayList(int id, ModelType modelType);
        Task<WorkoutDay> AddWorkoutDay(WorkoutDay day);
        Task<WorkoutDay> UpdateWorkoutDay(WorkoutDay day);
        Task<WorkoutDay> RemoveWorkoutDay(WorkoutDay day);
    }
}
