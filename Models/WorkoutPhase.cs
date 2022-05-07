using System.ComponentModel.DataAnnotations;

namespace WorkoutPlannerWebApp.Models
{
    public class WorkoutPhase
    {
        public int Id { get; set; }
        public WorkoutProgram WorkoutProgram { get; set; }

        //public int Order { get; set; }

        [Required(ErrorMessage = "Please enter a name for the workout phase.")]
        [Display(Name = "Phase Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the duration of this workout phase in week(s).")]
        public string Duration { get; set; }
         
        [Display(Name = "Phase Details")]
        public string PhaseDetails { get; set; }
        
        public IEnumerable<WorkoutDay> WorkoutDays { get; set; }
    }
}
