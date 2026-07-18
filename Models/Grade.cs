using System.ComponentModel.DataAnnotations;
namespace GradeBook.Models;

public class Grade
{
    public int Id { get; set; }

    [Required]
    public int StudentId { get; set; }

    [Required]
    public int AssignmentId { get; set; }

    [Required]
    [StringLength(50)]
    public string Score { get; set; } = string.Empty;

    public Student? Student { get; set; }
    public Assignment? Assignment { get; set; }
}
