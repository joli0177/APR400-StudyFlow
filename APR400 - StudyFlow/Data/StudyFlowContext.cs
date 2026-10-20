using Microsoft.EntityFrameworkCore;
using APR400___StudyFlow.Models;

namespace APR400___StudyFlow.Data;

public class StudyFlowContext : DbContext
{
    public StudyFlowContext(DbContextOptions<StudyFlowContext> options) : base(options) { }

    // DbSet blir två seperata tabeller en kurs och en uppgifter
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<StudyTask> Tasks => Set<StudyTask>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Här skapas 1:N kopplinegn då 1 kurs kan ha flera uppgifter men 1 uppgift har bara 1 kurs. 
        modelBuilder.Entity<StudyTask>()
            .HasOne(t => t.Course)
            .WithMany(c => c.Tasks)
            .HasForeignKey(t => t.CourseId)
            .OnDelete(DeleteBehavior.Cascade); // här tas både kurs och uppgifter bort då de är koppplade 

        // Spara status som text ("InProgress") i stället för en siffra
        modelBuilder.Entity<StudyTask>()
            .Property(t => t.Status)
            .HasConversion<string>();
    }
}