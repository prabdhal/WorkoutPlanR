using System.ComponentModel.DataAnnotations;
using WorkoutPlannerWebApp.HelperMethods;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkoutPlannerWebApp.Data;

namespace WorkoutPlannerWebApp.Models
{
  public class WorkoutProgram
  {
    [Key]
    public string Id { get; set; }

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
    
    public DateTime CreatedOn { get; set; }
    
    public DateTime UpdatedOn { get; set; }
    
    public ApplicationUser Creator { get; set; }
    
    public bool Published { get; set; }
  }
}
