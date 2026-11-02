using Microsoft.EntityFrameworkCore;
using APR400___StudyFlow.Data;
using APR400___StudyFlow.Models;

namespace APR400___StudyFlow.Services;

// All databaslogik för kurser. Varje metod skapar en egen, kortlivad koppling via factoryn.
public class CourseService(IDbContextFactory<StudyFlowContext> factory)
{
    // Hämtar alla kurser med deras examinationer (bara för att visa, därför AsNoTracking)
    public async Task<List<Course>> GetAllAsync()
    {
        using var db = await factory.CreateDbContextAsync();
        return await db.Courses
            .Include(c => c.Examinations)
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    // Skapa kurs
    public async Task AddAsync(Course course)
    {
        using var db = await factory.CreateDbContextAsync();
        db.Courses.Add(course);
        await db.SaveChangesAsync();
    }

    // Redigera kurs. Returnerar false om kursen inte finns.
    public async Task<bool> UpdateAsync(Course updated)
    {
        using var db = await factory.CreateDbContextAsync();
        var course = await db.Courses.FindAsync(updated.Id);
        if (course is null) return false;

        course.Name = updated.Name;
        course.CourseCode = updated.CourseCode;
        course.StartDate = updated.StartDate;
        course.EndDate = updated.EndDate;
        course.StudyPace = updated.StudyPace;

        await db.SaveChangesAsync();
        return true;
    }

    // Har kursen uppgifter? Används för att stoppa borttagning.
    public async Task<bool> HasTasksAsync(int courseId)
    {
        using var db = await factory.CreateDbContextAsync();
        return await db.Tasks.AnyAsync(t => t.CourseId == courseId);
    }

    // Ta bort kurs. Examinationerna tas bort automatiskt (Cascade).
    public async Task DeleteAsync(int courseId)
    {
        using var db = await factory.CreateDbContextAsync();
        await db.Courses.Where(c => c.Id == courseId).ExecuteDeleteAsync();
    }
}