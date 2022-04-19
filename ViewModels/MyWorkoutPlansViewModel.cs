#nullable disable
using WorkoutPlannerWebApp.Models;

namespace WorkoutPlannerWebApp.ViewModels
{
  public class MyWorkoutPlansViewModel
  {
    public IEnumerable<WorkoutProgram> WorkoutPrograms { get; set; }
  }
}
