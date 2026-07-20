using GradeBook.Data;
using GradeBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradesController(GradeBookContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Grade>>> GetGrades()
    {
        return Ok(await context.Grades.AsNoTracking().ToListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Grade>> GetGrade(int id)
    {
        var grade = await context.Grades.AsNoTracking().FirstOrDefaultAsync(g => g.Id == id);
        return grade is null ? NotFound() : Ok(grade);
    }

    [HttpPost]
    public async Task<ActionResult<Grade>> CreateGrade(Grade grade)
    {
        var studentExists = await context.Students.AsNoTracking().AnyAsync(s => s.Id == grade.StudentId);
        if (!studentExists)
        {
            return BadRequest($"Student with id {grade.StudentId} does not exist.");
        }

        var assignmentExists = await context.Assignments.AsNoTracking().AnyAsync(a => a.Id == grade.AssignmentId);
        if (!assignmentExists)
        {
            return BadRequest($"Assignment with id {grade.AssignmentId} does not exist.");
        }

        context.Grades.Add(grade);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetGrade), new { id = grade.Id }, grade);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateGrade(int id, Grade grade)
    {
        if (id != grade.Id)
        {
            return BadRequest("Route id and grade id must match.");
        }

        var existingGrade = await context.Grades.FirstOrDefaultAsync(g => g.Id == id);
        if (existingGrade is null)
        {
            return NotFound();
        }

        var studentExists = await context.Students.AsNoTracking().AnyAsync(s => s.Id == grade.StudentId);
        if (!studentExists)
        {
            return BadRequest($"Student with id {grade.StudentId} does not exist.");
        }

        var assignmentExists = await context.Assignments.AsNoTracking().AnyAsync(a => a.Id == grade.AssignmentId);
        if (!assignmentExists)
        {
            return BadRequest($"Assignment with id {grade.AssignmentId} does not exist.");
        }

        existingGrade.StudentId = grade.StudentId;
        existingGrade.AssignmentId = grade.AssignmentId;
        existingGrade.Score = grade.Score;

        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGrade(int id)
    {
        var grade = await context.Grades.FirstOrDefaultAsync(g => g.Id == id);
        if (grade is null)
        {
            return NotFound();
        }

        context.Grades.Remove(grade);
        await context.SaveChangesAsync();

        return NoContent();
    }
}
