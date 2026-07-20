using GradeBook.Data;
using GradeBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssignmentsController(GradeBookContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignments()
    {
        return Ok(await context.Assignments.AsNoTracking().ToListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Assignment>> GetAssignment(int id)
    {
        var assignment = await context.Assignments.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        return assignment is null ? NotFound() : Ok(assignment);
    }

    [HttpGet("{id:int}/grades")]
    public async Task<ActionResult<IEnumerable<Grade>>> GetAssignmentGrades(int id)
    {
        var assignmentExists = await context.Assignments.AsNoTracking().AnyAsync(a => a.Id == id);
        if (!assignmentExists)
        {
            return NotFound();
        }

        var grades = await context.Grades
            .AsNoTracking()
            .Where(g => g.AssignmentId == id)
            .ToListAsync();

        return Ok(grades);
    }

    [HttpPost]
    public async Task<ActionResult<Assignment>> CreateAssignment(Assignment assignment)
    {
        context.Assignments.Add(assignment);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAssignment), new { id = assignment.Id }, assignment);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAssignment(int id, Assignment assignment)
    {
        if (id != assignment.Id)
        {
            return BadRequest("Route id and assignment id must match.");
        }

        var existingAssignment = await context.Assignments.FirstOrDefaultAsync(a => a.Id == id);
        if (existingAssignment is null)
        {
            return NotFound();
        }

        existingAssignment.Title = assignment.Title;
        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAssignment(int id)
    {
        var assignment = await context.Assignments.FirstOrDefaultAsync(a => a.Id == id);
        if (assignment is null)
        {
            return NotFound();
        }

        context.Assignments.Remove(assignment);
        await context.SaveChangesAsync();

        return NoContent();
    }
}
