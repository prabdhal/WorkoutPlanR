using WorkoutPlannerWebApp.Models;

namespace WorkoutPlannerWebApp.ViewModels.MyWorkoutProgramsViewModels
{
    public class EditWorkoutDayViewModel
    {
        public WorkoutProgram WorkoutProgram { get; set; }
        public WorkoutPhase WorkoutPhase { get; set; }
        public WorkoutDay WorkoutDay { get; set; }
    }
}
