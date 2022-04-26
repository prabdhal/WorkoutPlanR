using WorkoutPlannerWebApp.Models;

namespace WorkoutPlannerWebApp.ViewModels
{
  public class MyWorkoutPlanCreateExerciseViewModel
  {
    public WorkoutProgram WorkoutProgram { get; set; }
    public IEnumerable<ExerciseAPI> ExerciseAPIs { get; set; }
    public Exercise Exercise { get; set; }
  }
}
