#nullable disable
using WorkoutPlannerWebApp.Models;

namespace WorkoutPlannerWebApp.ViewModels
{
  public class IndexMyWorkoutProgramViewModel
  {
    public IEnumerable<WorkoutProgram> WorkoutPrograms { get; set; }
  }
}
