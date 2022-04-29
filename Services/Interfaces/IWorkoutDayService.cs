using WorkoutPlannerWebApp.Models;

namespace WorkoutPlannerWebApp.Services.Interfaces
{
    public interface IWorkoutDayService
    {
        WorkoutDay GetWorkoutDay(int phaseId);
        IEnumerable<WorkoutDay> GetWorkoutDayFromProgramList(int phaseId);
        IEnumerable<WorkoutDay> GetWorkoutDayFromPhaseList(int programId);
        Task<WorkoutDay> AddWorkoutDay(WorkoutDay day);
        WorkoutDay UpdateWorkoutDay(WorkoutDay day);
        Task<WorkoutDay> RemoveWorkoutDay(WorkoutDay day);
    }
}
