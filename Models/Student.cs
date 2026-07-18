using System.ComponentModel.DataAnnotations;
namespace GradeBook.Models;



public class Student
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(240)]
    public string Email { get; set; } = string.Empty;

    public ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
