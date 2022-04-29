using WorkoutPlannerWebApp.Models;

namespace WorkoutPlannerWebApp.ViewModels
{
  public class CreateExerciseMyWorkoutProgramViewModel
  {
    public WorkoutProgram WorkoutProgram { get; set; }
    public IEnumerable<Exercise> Exercises { get; set; }
    public IEnumerable<CustomExercise> CustomExercises { get; set; }
    public CustomExercise CustomExercise { get; set; }
  }
}
