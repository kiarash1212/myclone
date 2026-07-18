using GradeBook.Models;
using Microsoft.EntityFrameworkCore;
namespace GradeBook.Data;

public class GradeBookContext(DbContextOptions<GradeBookContext> options) : DbContext(options)
{
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Assignment> Assignments => Set<Assignment>();
    public DbSet<Grade> Grades => Set<Grade>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().ToTable("Students");
        modelBuilder.Entity<Assignment>().ToTable("Assignments");
        modelBuilder.Entity<Grade>().ToTable("Grades");

        modelBuilder.Entity<Grade>()
            .HasOne(g => g.Student)
            .WithMany(s => s.Grades)
            .HasForeignKey(g => g.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Grade>()
            .HasOne(g => g.Assignment)
            .WithMany(a => a.Grades)
            .HasForeignKey(g => g.AssignmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
