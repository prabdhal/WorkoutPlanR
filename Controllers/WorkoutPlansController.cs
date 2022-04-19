using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkoutPlannerWebApp.Data;
using WorkoutPlannerWebApp.ViewModels;

namespace WorkoutPlannerWebApp.Controllers
{
  public class WorkoutPlansController : Controller
  {
    private readonly ApplicationDbContext applicationDbContext;

    public WorkoutPlansController(ApplicationDbContext applicationDbContext)
    {
      this.applicationDbContext = applicationDbContext;
    }

    // GET: WorkoutPlansController
    public ActionResult Index()
    {
      var workoutPrograms = applicationDbContext.WorkoutPrograms
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
      return View();
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
