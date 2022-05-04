﻿#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkoutPlannerWebApp.BusinessManager.Interfaces;
using WorkoutPlannerWebApp.Models;
using WorkoutPlannerWebApp.ViewModels;
using WorkoutPlannerWebApp.ViewModels.MyWorkoutProgramsViewModels;

namespace WorkoutPlannerWebApp.Controllers
{
    [Authorize]
    public class MyWorkoutProgramsController : Controller
    {
        private readonly IMyWorkoutProgramBusinessManager workoutProgramBusinessManager;
        private readonly IMyWorkoutPhaseBusinessManager workoutPhaseBusinessManager;
        private readonly IExerciseBusinessManager exerciseBusinessManager;

        public MyWorkoutProgramsController(
            IMyWorkoutProgramBusinessManager workoutProgramBusinessManager,
            IMyWorkoutPhaseBusinessManager workoutPhaseBusinessManager,
            IExerciseBusinessManager exerciseBusinessManager)
        {
            this.workoutProgramBusinessManager = workoutProgramBusinessManager;
            this.workoutPhaseBusinessManager = workoutPhaseBusinessManager;
            this.exerciseBusinessManager = exerciseBusinessManager;
        }

        // GET: MyWorkoutPlans
        public IActionResult Index(string search)
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

            return RedirectToAction("CreateWorkoutPhase", new { createViewModel.WorkoutProgram.Id });
        }

        // GET: MyWorkoutPlans/CreateExercise/id
        public IActionResult CreateWorkoutPhase(int id)
        {
            var createViewModel = workoutPhaseBusinessManager.GetCreateWorkoutPhaseViewModel(id);

            return View(createViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWorkoutPhase(CreateWorkoutPhaseViewModel createViewModel)
        {
            var exercise = await workoutPhaseBusinessManager.CreateWorkoutPhase(createViewModel);

            return RedirectToAction("ViewWorkoutPhases", new { createViewModel.WorkoutProgram.Id });
        }

        public IActionResult ViewWorkoutPhases(int id)
        {
            var viewModel = workoutPhaseBusinessManager.GetViewWorkoutPhasesViewModel(id);

            return View(viewModel);
        }

        // GET: MyWorkoutPlans/CreateExercise/id
        public IActionResult CreateExercise(int id)
        {
            var createViewModel = exerciseBusinessManager.GetCreateExerciseMyWorkoutProgramsViewModel(id, ModelType.WorkoutDay, out IEnumerable<Exercise> exercises);

            ViewBag.Exercises = new SelectList(exercises, "Id", "Name");

            return View(createViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExercise(CreateExerciseViewModel createViewModel)
        {
            var exercise = await exerciseBusinessManager.CreateCustomExercise(createViewModel);

            ModelState.Clear();
            return RedirectToAction("CreateExercise", new { exercise.WorkoutDay.Id });
        }

        // GET: MyWorkoutPlans/Details/5
        public IActionResult Details(int id)
        {
            var detailViewModel = workoutProgramBusinessManager.GetDetailMyWorkoutProgramsViewModel(id);

            return View(detailViewModel);
        }

        // GET: MyWorkoutPlans/Edit/5
        public IActionResult Edit(int id)
        {
            var editViewModel = workoutProgramBusinessManager.GetEditMyWorkoutProgramsViewModel(id);

            return View(editViewModel);
        }

        // POST: MyWorkoutPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditMyWorkoutProgramViewModel editViewModel)
        {
            var program = workoutProgramBusinessManager.EditWorkoutProgram(editViewModel);

            return RedirectToAction("ViewWorkoutPhases", new { editViewModel.WorkoutProgram.Id });
        }

        public IActionResult EditWorkoutDay(int id)
        {
            var editViewModel = workoutPhaseBusinessManager.GetEditWorkoutDayViewModel(id);

            return View(editViewModel);
        }

        // POST: MyWorkoutPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditWorkoutDay(int id, CreateExerciseViewModel editViewModel)
        {
            var day = workoutPhaseBusinessManager.EditWorkoutDay(editViewModel);

            return RedirectToAction("ViewWorkoutPhases", new { editViewModel.WorkoutProgram.Id });
        }

        public IActionResult EditWorkoutPhase(int id)
        {
            var editViewModel = workoutPhaseBusinessManager.GetEditWorkoutPhaseViewModel(id);

            return View(editViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditWorkoutPhase(EditWorkoutPhaseViewModel editViewModel)
        {
            var exercise = workoutPhaseBusinessManager.EditWorkoutPhase(editViewModel);

            return RedirectToAction("ViewWorkoutPhases", new { editViewModel.WorkoutProgram.Id });
        }

        // POST: MyWorkoutPlans/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWorkoutProgram(int id)
        {
            var program = await workoutProgramBusinessManager.DeleteWorkoutProgram(id);

            return RedirectToAction("Index");
        }

        // POST: MyWorkoutPlans/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWorkoutPhase(int id)
        {

            var phase = workoutPhaseBusinessManager.GetWorkoutPhase(id, ModelType.WorkoutPhase);   
            var program = workoutProgramBusinessManager.GetWorkoutProgram(phase.WorkoutProgram.Id);
            phase = await workoutPhaseBusinessManager.DeleteWorkoutPhase(id);

            return RedirectToAction("ViewWorkoutPhases", new { program.Id });
        }

        // POST: MyWorkoutPlans/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ClearWorkoutDay(int id)
        {
            var day = workoutPhaseBusinessManager.GetWorkoutDay(id, ModelType.WorkoutDay);
            var program = workoutProgramBusinessManager.GetWorkoutProgram(day.WorkoutProgram.Id);
            var d = workoutPhaseBusinessManager.ClearWorkoutDay(id);

            return RedirectToAction("ViewWorkoutPhases", new { program.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            var exercise = exerciseBusinessManager.GetCustomExercise(id);
            var program = workoutProgramBusinessManager.GetWorkoutProgram(exercise.WorkoutProgram.Id);
            exercise = await exerciseBusinessManager.DeleteCustomExercise(id, ModelType.CustomExercise);

            return RedirectToAction("CreateExercise", new { exercise.WorkoutDay.Id });
        }
    }
}
