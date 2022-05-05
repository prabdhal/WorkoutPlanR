using Microsoft.AspNetCore.Mvc;
using WorkoutPlannerWebApp.BusinessManager.Interfaces;

namespace WorkoutPlannerWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWorkoutProgramBusinessManager workoutProgramsBusinessManager;
        private readonly IExerciseBusinessManager exerciseBusinessManager;

        public IExerciseBusinessManager ExerciseBusinessManager => exerciseBusinessManager;

        public HomeController(
            IWorkoutProgramBusinessManager workoutProgramBusinessManager,
            IExerciseBusinessManager exerciseBusinessManager)
        {
            this.workoutProgramsBusinessManager = workoutProgramBusinessManager;
            this.exerciseBusinessManager = exerciseBusinessManager;
        }

        // GET: WorkoutPlansController
        public ActionResult Index(string searchString)
        {
            var indexViewModel = workoutProgramsBusinessManager.GetIndexWorkoutProgramsViewModel(searchString);

            return View(indexViewModel);
        }

        // GET: WorkoutPlansController/Details/5
        public ActionResult Details(int id)
        {
            var detailViewModel = workoutProgramsBusinessManager.GetDetailWorkoutProgramsViewModel(id);

            return View(detailViewModel);
        }
        
        public ActionResult PhaseDetails(int id)
        {
            var detailViewModel = workoutProgramsBusinessManager.GetPhaseDetailWorkoutProgramsViewModel(id);

            return View(detailViewModel);
        }

        // GET: WorkoutPlansController/Details/5
        public ActionResult FullDetails(int id)
        {
            var detailViewModel = workoutProgramsBusinessManager.GetFullDetailWorkoutProgramsViewModel(id);

            return View(detailViewModel);
        }
    }
}
