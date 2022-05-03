#nullable disable
using WorkoutPlannerWebApp.Models;

namespace WorkoutPlannerWebApp.ViewModels
{
  public class IndexViewModel
  {
    public IEnumerable<WorkoutProgram> WorkoutPrograms { get; set; }
  }
}
