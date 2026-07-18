using System.ComponentModel.DataAnnotations;
namespace GradeBook.Models;

public class Assignment
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    public ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
