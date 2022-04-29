#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkoutPlannerWebApp.BusinessManager.Interfaces;
using WorkoutPlannerWebApp.Models;
using WorkoutPlannerWebApp.ViewModels;

namespace WorkoutPlannerWebApp.Controllers
{
    [Authorize]
    public class MyWorkoutProgramsController : Controller
    {
        private readonly IMyWorkoutProgramBusinessManager workoutProgramBusinessManager;
        private readonly IExerciseBusinessManager exerciseBusinessManager;

        public MyWorkoutProgramsController(
            IMyWorkoutProgramBusinessManager workoutProgramBusinessManager,
            IExerciseBusinessManager exerciseBusinessManager)
        {
            this.workoutProgramBusinessManager = workoutProgramBusinessManager;
            this.exerciseBusinessManager = exerciseBusinessManager;
        }

        // GET: MyWorkoutPlans
        public async Task<IActionResult> Index(string search)
        {
            var indexViewModel = workoutProgramBusinessManager.GetIndexMyWorkoutProgramsViewModel(search);

            return View(indexViewModel);
        }

        // GET: MyWorkoutPlans/Create
        public IActionResult Create()
        {
            var createViewModel = workoutProgramBusinessManager.GetCreateMyWorkoutProgramsViewModel();

            return View(createViewModel);
        }

        // POST: MyWorkoutPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMyWorkoutProgramViewModel createViewModel)
        {
            await workoutProgramBusinessManager.CreateWorkoutProgram(createViewModel, User);

            return RedirectToAction("CreateExercise", new { createViewModel.WorkoutProgram.Id });
        }

        // GET: MyWorkoutPlans/CreateExercise/id
        public IActionResult CreateExercise(int id)
        {
            var createViewModel = exerciseBusinessManager.GetCreateExerciseMyWorkoutProgramsViewModel(id, out IEnumerable<Exercise> exercises);

            ViewBag.Exercises = new SelectList(exercises, "Id", "Name");

            return View(createViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExercise(CreateExerciseMyWorkoutProgramViewModel createViewModel)
        {
            var exercise = await exerciseBusinessManager.CreateCustomExercise(createViewModel);

            ModelState.Clear();
            return RedirectToAction("CreateExercise", new { createViewModel.WorkoutProgram.Id });
        }

        // GET: MyWorkoutPlans/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var detailViewModel = workoutProgramBusinessManager.GetDetailMyWorkoutProgramsViewModel(id);

            return View(detailViewModel);
        }

        // GET: MyWorkoutPlans/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var editViewModel = workoutProgramBusinessManager.GetEditMyWorkoutProgramsViewModel(id);

            return View(editViewModel);
        }

        // POST: MyWorkoutPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditMyWorkoutProgramViewModel editViewModel)
        {
            var program = workoutProgramBusinessManager.EditWorkoutProgram(editViewModel);

            return RedirectToAction("CreateExercise", new { editViewModel.WorkoutProgram.Id });
        }

        // POST: MyWorkoutPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWorkoutProgram(int id)
        {
            var program = await workoutProgramBusinessManager.DeleteWorkoutProgram(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            var exercise = await exerciseBusinessManager.DeleteCustomExercise(id);
            var program = workoutProgramBusinessManager.GetWorkoutProgram(id);

            return RedirectToAction("CreateExercise", new { program.Id });
        }
    }
}
