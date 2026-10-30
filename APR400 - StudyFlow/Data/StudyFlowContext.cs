using Microsoft.EntityFrameworkCore;
using APR400___StudyFlow.Models;

namespace APR400___StudyFlow.Data;

public class StudyFlowContext : DbContext
{
    public StudyFlowContext(DbContextOptions<StudyFlowContext> options) : base(options) { }

    // Varje DbSet blir en tabell i databasen
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<StudyTask> Tasks => Set<StudyTask>();
    public DbSet<Examination> Examinations => Set<Examination>(); // NY

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 1:N, en kurs kan ha många uppgifter men en uppgift har bara en kurs
        modelBuilder.Entity<StudyTask>()
            .HasOne(t => t.Course)
            .WithMany(c => c.Tasks)
            .HasForeignKey(t => t.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        // Spara status som text ("InProgress") i stället för en siffra
        modelBuilder.Entity<StudyTask>()
            .Property(t => t.Status)
            .HasConversion<string>();

        // NY: 1:N, en kurs kan ha många examinationer men en examination har bara en kurs
        modelBuilder.Entity<Examination>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Examinations)
            .HasForeignKey(e => e.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        // NY: Spara examinationstypen som text ("Exam") i stället för en siffra
        modelBuilder.Entity<Examination>()
            .Property(e => e.Type)
            .HasConversion<string>();
    }
}