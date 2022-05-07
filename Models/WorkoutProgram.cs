#nullable disable
using System.ComponentModel.DataAnnotations;
using WorkoutPlannerWebApp.HelperMethods;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkoutPlannerWebApp.Data;

namespace WorkoutPlannerWebApp.Models
{
    public class WorkoutProgram
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name for the workout program.")]
        [Display(Name = "Program Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the difficulty of the workout program.")]
        public Difficulty Difficulty { get; set; }

        [Required(ErrorMessage = "Please enter a short description of the workout program.")]
        [StringLength(250, ErrorMessage = "The {0} must be less than {1} characters")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Program Details")]
        public string ProgramDetails { get; set; }

        [Display(Name = "Created On")]
        [DisplayFormat(DataFormatString = "{0:MMM dd hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Last Updated")]
        [DisplayFormat(DataFormatString = "{0:MMM dd hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedOn { get; set; }

        public ApplicationUser Publisher { get; set; }

        public bool Published { get; set; }

        public IEnumerable<WorkoutPhase> WorkoutPhases { get; set; }
    }

    public enum ModelType
    {
        WorkoutProgram,
        WorkoutPhase,
        WorkoutDay,
        CustomExercise,
        Exercise
    }

}
