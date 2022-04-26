using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutPlannerWebApp.Models
{
  public class Exercise
  {
    public int Id { get; set; }

    [ForeignKey("Workout Program")]
    public WorkoutProgram WorkoutProgram { get; set; }

    [Display(Name = "Exercise")]
    [ForeignKey("Workout Program")]
    [Required(ErrorMessage = "Please select an exercise.")]
    public ExerciseAPI ExerciseAPI { get; set; }

    [Range(1, 20)]
    [Required(ErrorMessage = "Please enter the number of sets for this exercise.")]
    public int Sets { get; set; }

    [Range(1, 50)]
    [Required(ErrorMessage = "Please enter the minimum number of repetitions for each set.")]
    [Display(Name = "Minimum Repetitions")]
    public int MinRepetition { get; set; }

    [Range(0, 50)]
    [Display(Name = "Maximum Repetitions")]
    public int? MaxRepetition { get; set; }
  }
}
