#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkoutPlannerWebApp.Data;
using WorkoutPlannerWebApp.Models;
using WorkoutPlannerWebApp.ViewModels;

namespace WorkoutPlannerWebApp.Controllers
{
  [Authorize]
  public class MyWorkoutPlansController : Controller
  {
    private readonly ApplicationDbContext _context;

    public MyWorkoutPlansController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: MyWorkoutPlans
    public async Task<IActionResult> Index()
    {
      var workoutPrograms = _context.WorkoutPrograms;

      if (workoutPrograms is null)
        return new BadRequestResult();

      var myWorkoutPlansViewModel = new MyWorkoutPlansViewModel
      {
        WorkoutPrograms = workoutPrograms,
      };

      return View(myWorkoutPlansViewModel);
    }

    // GET: MyWorkoutPlans/Details/5
    public async Task<IActionResult> Details(string id)
    {
      if (id is null)
        return new BadRequestResult();

      var workoutProgram = _context.WorkoutPrograms
        .FirstOrDefault(p => p.Id == id);

      if (workoutProgram is null)
        return new NotFoundResult();

      var myWorkoutPlanDetailViewModel = new MyWorkoutPlanDetailViewModel
      {
        WorkoutProgram = workoutProgram,
      };

      return View(myWorkoutPlanDetailViewModel);
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
    public async Task<IActionResult> Create([Bind("Id,Name,Difficulty,ShortDescription,LongDescription,CreatedOn,UpdatedOn,Published")] WorkoutProgram workoutProgram)
    {
      if (ModelState.IsValid)
      {
        _context.Add(workoutProgram);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(workoutProgram);
    }

    // GET: MyWorkoutPlans/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var workoutProgram = await _context.WorkoutPrograms.FindAsync(id);
      if (workoutProgram == null)
      {
        return NotFound();
      }
      return View(workoutProgram);
    }

    // POST: MyWorkoutPlans/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Difficulty,ShortDescription,LongDescription,CreatedOn,UpdatedOn,Published")] WorkoutProgram workoutProgram)
    {
      if (id != workoutProgram.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(workoutProgram);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!WorkoutProgramExists(workoutProgram.Id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      return View(workoutProgram);
    }

    // GET: MyWorkoutPlans/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var workoutProgram = await _context.WorkoutPrograms
          .FirstOrDefaultAsync(m => m.Id == id);
      if (workoutProgram == null)
      {
        return NotFound();
      }

      return View(workoutProgram);
    }

    // POST: MyWorkoutPlans/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
      var workoutProgram = await _context.WorkoutPrograms.FindAsync(id);
      _context.WorkoutPrograms.Remove(workoutProgram);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool WorkoutProgramExists(string id)
    {
      return _context.WorkoutPrograms.Any(e => e.Id == id);
    }
  }
}
