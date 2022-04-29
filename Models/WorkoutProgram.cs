#nullable disable
using System.ComponentModel.DataAnnotations;
using WorkoutPlannerWebApp.HelperMethods;

namespace WorkoutPlannerWebApp.Models
{
  public class WorkoutProgram
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter a name for the workout program.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Please enter the difficulty of the workout program.")]
    public Difficulty Difficulty { get; set; }

    [Required(ErrorMessage = "Please enter a short description of the workout program.")]
    [StringLength(100, ErrorMessage = "The {1} must be less than {2} characters")]
    [Display(Name = "Short Description")]
    public string ShortDescription { get; set; }

    [Display(Name = "Long Description")]
    public string LongDescription { get; set; }

    [Display(Name = "Created On")]
    [DisplayFormat(DataFormatString = "{0:MMM dd hh:mm tt}", ApplyFormatInEditMode = true)]
    public DateTime CreatedOn { get; set; }

    [Display(Name = "Last Updated")]
    [DisplayFormat(DataFormatString = "{0:MMM dd hh:mm tt}", ApplyFormatInEditMode = true)]
    public DateTime UpdatedOn { get; set; }

    public ApplicationUser Publisher { get; set; }

    public bool Published { get; set; }

    public IEnumerable<CustomExercise> CustomExercises { get; set; }
  }
}
