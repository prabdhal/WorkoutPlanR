using Microsoft.AspNetCore.Mvc;
using WorkoutPlannerWebApp.BusinessManager.Interfaces;

namespace WorkoutPlannerWebApp.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly IExerciseBusinessManager exerciseBusinessManager;

        public ExercisesController(IExerciseBusinessManager exerciseBusinessManager)
        {
            this.exerciseBusinessManager = exerciseBusinessManager;
        }

        // GET: Exercises
        public IActionResult Index(string searchString, int page = 0)
        {
            var indexViewModel = exerciseBusinessManager.GetIndexExercisesViewModel(searchString, page);

            return View(indexViewModel);
        }

        // GET: Exercises/Details/5
        public IActionResult Details(int id)
        {
            var detailViewModel = exerciseBusinessManager.GetDetailExerciseViewModel(id);

            return View(detailViewModel);
        }
    }
}
