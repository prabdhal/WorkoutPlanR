using System.ComponentModel.DataAnnotations;

namespace WorkoutPlannerWebApp.Models
{
    public class WorkoutDay
    {
        public int Id { get; set; }
        public int DayNumber { get; set; }
        public WorkoutProgram WorkoutProgram { get; set; }
        public WorkoutPhase WorkoutPhase { get; set; }

        [Required(ErrorMessage = "Please enter a name for the workout day.")]
        public string Name { get; set; }

        public string Description { get; set; }
        public IEnumerable<CustomExercise> CustomExercises { get; set; }
    }
}
