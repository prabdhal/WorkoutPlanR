using System.ComponentModel.DataAnnotations;

namespace WorkoutPlannerWebApp.Models
{
  public class ExerciseAPI
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter the name of the exercise.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Please enter the description of the exercise.")]
    public string Description { get; set; }

    [Url]
    [Display(Name = "Reference Link")]
    public string ReferenceLink { get; set; }

    public IEnumerable<Exercise>? Exercises { get; set; }
  }
}
