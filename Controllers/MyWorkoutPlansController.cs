#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WorkoutPlannerWebApp.Data;
using WorkoutPlannerWebApp.Models;
using WorkoutPlannerWebApp.ViewModels;

namespace WorkoutPlannerWebApp.Controllers
{
  [Authorize]
  public class MyWorkoutPlansController : Controller
  {
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _user;

    public MyWorkoutPlansController(ApplicationDbContext context, UserManager<ApplicationUser> user)
    {
      _context = context;
      _user = user;
    }

    // GET: MyWorkoutPlans
    public async Task<IActionResult> Index()
    {
      var workoutPrograms = _context.WorkoutPrograms
        .OrderByDescending(p => p.UpdatedOn);

      if (workoutPrograms is null)
        return new BadRequestResult();

      var workoutPlansViewModel = new MyWorkoutPlansViewModel
      {
        WorkoutPrograms = workoutPrograms,
      };

      return View(workoutPlansViewModel);
    }

    // GET: MyWorkoutPlans/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: MyWorkoutPlans/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MyWorkoutPlanCreateViewModel createViewModel)
    {
      var workoutProgram = createViewModel.WorkoutProgram;

      workoutProgram.Publisher = await _user.GetUserAsync(User);
      workoutProgram.CreatedOn = DateTime.Now;
      workoutProgram.UpdatedOn = DateTime.Now;

      _context.WorkoutPrograms.Add(workoutProgram);
      await _context.SaveChangesAsync();
      return RedirectToAction("CreateExercise", new { createViewModel.WorkoutProgram.Id });
    }

    // GET: MyWorkoutPlans/CreateExercise/id
    public IActionResult CreateExercise(int id)
    {
      var workoutProgram = _context.WorkoutPrograms
        .Include(p => p.Publisher)
        .Include(p => p.Exercises)
        .FirstOrDefault(p => p.Id == id);

      if (workoutProgram is null)
        return new NotFoundResult();

      var createExerciseViewModel = new MyWorkoutPlanCreateExerciseViewModel
      {
        WorkoutProgram = workoutProgram,
        Exercise = null
      };

      return View(createExerciseViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateExercise(MyWorkoutPlanCreateExerciseViewModel createViewModel)
    {
      var workoutProgram = _context.WorkoutPrograms
        .Include(p => p.Publisher)
        .Include(p => p.Exercises)
        .FirstOrDefault(p => p.Id == createViewModel.WorkoutProgram.Id);

      if (workoutProgram is null)
        return new NotFoundResult();

      var exercise = createViewModel.Exercise;

      exercise.WorkoutProgram = workoutProgram;

      _context.Exercises.Add(exercise);
      await _context.SaveChangesAsync();
      ModelState.Clear();
      return RedirectToAction("CreateExercise", new { createViewModel.WorkoutProgram.Id });
    }

    // GET: MyWorkoutPlans/Details/5
    public async Task<IActionResult> Details(int id)
    {
      var workoutProgram = _context.WorkoutPrograms
        .Include(p => p.Publisher)
        .Include(p => p.Exercises)
        .FirstOrDefault(p => p.Id == id);

      if (workoutProgram is null)
        return new NotFoundResult();

      var detailViewModel = new MyWorkoutPlanDetailViewModel
      {
        WorkoutProgram = workoutProgram,
      };

      return View(detailViewModel);
    }

    // GET: MyWorkoutPlans/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
      var workoutProgram = _context.WorkoutPrograms
        .Include(p => p.Publisher)
        .Include(p => p.Exercises)
        .FirstOrDefault(p => p.Id == id);

      if (workoutProgram is null)
        return new NotFoundResult();

      var editViewModel = new MyWorkoutPlanEditViewModel
      {
        WorkoutProgram = workoutProgram,
      };

      return View(editViewModel);
    }

    // POST: MyWorkoutPlans/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, MyWorkoutPlanEditViewModel editViewModel)
    {
      var workoutProgram = _context.WorkoutPrograms
        .FirstOrDefault(p => p.Id == editViewModel.WorkoutProgram.Id);

      if (workoutProgram is null)
        return RedirectToAction("Details", new { editViewModel.WorkoutProgram.Id });

      workoutProgram.Name = editViewModel.WorkoutProgram.Name;
      workoutProgram.ShortDescription = editViewModel.WorkoutProgram.ShortDescription;
      workoutProgram.LongDescription = editViewModel.WorkoutProgram.LongDescription;
      workoutProgram.Difficulty = editViewModel.WorkoutProgram.Difficulty;
      workoutProgram.Published = editViewModel.WorkoutProgram.Published;
      workoutProgram.UpdatedOn = DateTime.Now;

      _context.WorkoutPrograms.Update(workoutProgram);
      await _context.SaveChangesAsync();

      var updatedViewModel = new MyWorkoutPlanEditViewModel
      {
        WorkoutProgram = workoutProgram,
      };

      return RedirectToAction("CreateExercise", new { editViewModel.WorkoutProgram.Id });
    }

    // POST: MyWorkoutPlans/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var workoutProgram = _context.WorkoutPrograms
        .Include(p => p.Publisher)
        .Include(p => p.Exercises)
        .FirstOrDefault(p => p.Id == id);

      if (workoutProgram.Exercises != null)
      {
        // delete all the exercises of the workout
        foreach (var exercise in workoutProgram.Exercises)
        {
          _context.Remove(exercise);
          await _context.SaveChangesAsync();
        }
      }

      // delete workout
      _context.WorkoutPrograms.Remove(workoutProgram);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteExerciseConfirmed(int id)
    {
      var exercise = _context.Exercises.FirstOrDefault(e => e.Id == id);

      var workoutProgram = _context.Exercises
        .Include(e => e.WorkoutProgram)
        .FirstOrDefault(e => e.Id == exercise.Id).WorkoutProgram;

      _context.Remove(exercise);
      await _context.SaveChangesAsync();
      return RedirectToAction("CreateExercise", new { workoutProgram.Id });
    }

    private bool WorkoutProgramExists(int id)
    {
      return _context.WorkoutPrograms.Any(e => e.Id == id);
    }
  }
}
