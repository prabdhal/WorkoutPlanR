using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutPlannerWebApp.Data;
using WorkoutPlannerWebApp.ViewModels;

namespace WorkoutPlannerWebApp.Controllers
{
  public class WorkoutPlansController : Controller
  {
    private readonly ApplicationDbContext _context;

    public WorkoutPlansController(ApplicationDbContext applicationDbContext)
    {
      this._context = applicationDbContext;
    }

    // GET: WorkoutPlansController
    public ActionResult Index()
    {
      var workoutPrograms = _context.WorkoutPrograms
        .OrderByDescending(p => p.UpdatedOn)
        .Include(p => p.Publisher)
        .Where(p => p.Published);

      var workoutPlansViewModel = new WorkoutPlansViewModel
      {
        WorkoutPrograms = workoutPrograms,
      };

      return View(workoutPlansViewModel);
    }

    // GET: WorkoutPlansController/Details/5
    public ActionResult Details(int id)
    {
      var workoutProgram = _context.WorkoutPrograms
        .Include(p => p.Publisher)
        .Include(p => p.Exercises)
        .FirstOrDefault(p => p.Id == id);

      if (workoutProgram is null)
        return new NotFoundResult();

      var workoutPlanDetailViewModel = new WorkoutPlanDetailViewModel
      {
        WorkoutProgram = workoutProgram
      };

      return View(workoutPlanDetailViewModel);
    }

    // GET: WorkoutPlansController/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: WorkoutPlansController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
    {
      try
      {
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }

    // GET: WorkoutPlansController/Edit/5
    public ActionResult Edit(int id)
    {
      return View();
    }

    // POST: WorkoutPlansController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
      try
      {
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }

    // GET: WorkoutPlansController/Delete/5
    public ActionResult Delete(int id)
    {
      return View();
    }

    // POST: WorkoutPlansController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
      try
      {
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }
  }
}
