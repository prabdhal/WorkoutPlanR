#nullable disable
using System.ComponentModel.DataAnnotations;
using WorkoutPlannerWebApp.Models;

namespace WorkoutPlannerWebApp.ViewModels
{
  public class WorkoutPlansViewModel
  {
    [Display(Name = "Search")]
    public string SearchString { get; set; }
    public IEnumerable<WorkoutProgram> WorkoutPrograms { get; set; }
  }
}
