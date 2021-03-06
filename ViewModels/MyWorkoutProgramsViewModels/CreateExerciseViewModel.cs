using WorkoutPlannerWebApp.Models;

namespace WorkoutPlannerWebApp.ViewModels
{
  public class CreateExerciseViewModel
  {
    public WorkoutProgram WorkoutProgram { get; set; }
    public WorkoutPhase WorkoutPhase { get; set; }
    public WorkoutDay WorkoutDay { get; set; }
    public IEnumerable<Exercise> Exercises { get; set; }
    public Exercise Exercise { get; set; }
    public IEnumerable<CustomExercise> CustomExercises { get; set; }
    public CustomExercise CustomExercise { get; set; }
  }
}
