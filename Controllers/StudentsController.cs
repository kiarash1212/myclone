using GradeBook.Data;
using GradeBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController(GradeBookContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        return Ok(await context.Students.AsNoTracking().ToListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
        var student = await context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        return student is null ? NotFound() : Ok(student);
    }

    [HttpGet("{id:int}/grades")]
    public async Task<ActionResult<IEnumerable<Grade>>> GetStudentGrades(int id)
    {
        var studentExists = await context.Students.AsNoTracking().AnyAsync(s => s.Id == id);
        if (!studentExists)
        {
            return NotFound();
        }
        var grades = await context.Grades
            .AsNoTracking()
            .Where(g => g.StudentId == id)
            .ToListAsync();

        return Ok(grades);
    }

    [HttpPost]
    public async Task<ActionResult<Student>> CreateStudent(Student student)
    {
        context.Students.Add(student);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateStudent(int id, Student student)
    {
        if (id != student.Id)
        {
            return BadRequest("Route id and student id must match.");
        }

        var existingStudent = await context.Students.FirstOrDefaultAsync(s => s.Id == id);
        if (existingStudent is null)
        {
            return NotFound();
        }

        existingStudent.FirstName = student.FirstName;
        existingStudent.LastName = student.LastName;
        existingStudent.Email = student.Email;

        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await context.Students.FirstOrDefaultAsync(s => s.Id == id);
        if (student is null)
        {
            return NotFound();
        }

        context.Students.Remove(student);
        await context.SaveChangesAsync();

        return NoContent();
    }
}
