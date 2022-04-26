#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutPlannerWebApp.Data;
using WorkoutPlannerWebApp.Models;

namespace WorkoutPlannerWebApp.Controllers.Api
{
  [Route("api/[controller]")]
  [ApiController]
  public class ExercisesController : ControllerBase
  {
    private readonly ApplicationDbContext _context;

    public ExercisesController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: api/Exercises
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExerciseAPI>>> GetExercises()
    {
      return await _context.ExerciseAPIs.ToListAsync();
    }

    // GET: api/Exercises/searchString
    [HttpGet("Search/{searchString}")]
    public async Task<ActionResult<IEnumerable<ExerciseAPI>>> GetExercises(string searchString)
    {
      return await _context.ExerciseAPIs.Where(e => e.Name.Contains(searchString) || e.Description.Contains(searchString)).ToListAsync();
    }

    // GET: api/Exercises/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ExerciseAPI>> GetExercise(int id)
    {
      var exercise = await _context.ExerciseAPIs.FindAsync(id);

      if (exercise == null)
      {
        return NotFound();
      }

      return exercise;
    }

    // PUT: api/Exercises/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutExercise(int id, ExerciseAPI exercise)
    {
      if (id != exercise.Id)
      {
        return BadRequest();
      }

      _context.Entry(exercise).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ExerciseExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/Exercises
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ExerciseAPI>> PostExercise(ExerciseAPI exercise)
    {
      _context.ExerciseAPIs.Add(exercise);
      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateException)
      {
        if (ExerciseExists(exercise.Id))
        {
          return Conflict();
        }
        else
        {
          throw;
        }
      }

      return CreatedAtAction("GetExercise", new { id = exercise.Id }, exercise);
    }

    // DELETE: api/Exercises/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExercise(string id)
    {
      var exercise = await _context.ExerciseAPIs.FindAsync(id);
      if (exercise == null)
      {
        return NotFound();
      }

      _context.ExerciseAPIs.Remove(exercise);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool ExerciseExists(int id)
    {
      return _context.ExerciseAPIs.Any(e => e.Id == id);
    }
  }
}
