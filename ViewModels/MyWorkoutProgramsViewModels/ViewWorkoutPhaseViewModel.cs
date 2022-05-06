using WorkoutPlannerWebApp.Models;

namespace WorkoutPlannerWebApp.ViewModels.MyWorkoutProgramsViewModels
{
    public class ViewWorkoutPhaseViewModel
    {
        public WorkoutProgram WorkoutProgram { get; set; }
        public IEnumerable<WorkoutPhase> WorkoutPhases { get; set; }
        public WorkoutPhase WorkoutPhase { get; set; }
        public int? CopyWorkoutLinkId { get; set; }
    }
}
