using WorkoutPlannerWebApp.Models;

namespace WorkoutPlannerWebApp.ViewModels.ExercisesViewModels
{
    public class IndexExercisesViewModel
    {
        public string SearchString { get; set; }
        public IEnumerable<Exercise> Exercises { get; set; }
    }
}
