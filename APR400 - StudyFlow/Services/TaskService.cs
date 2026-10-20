using Microsoft.EntityFrameworkCore;
using APR400___StudyFlow.Data;
using APR400___StudyFlow.Models;

namespace APR400___StudyFlow.Services;

public class TaskService(IDbContextFactory<StudyFlowContext> factory)
{
    // Visa uppgifter (och filtrera på kurs om courseId skickas med)
    public async Task<List<StudyTask>> GetAllAsync(int? courseId = null)
    {
        using var db = await factory.CreateDbContextAsync();

        var query = db.Tasks.Include(t => t.Course).AsQueryable();
        if (courseId is not null)
            query = query.Where(t => t.CourseId == courseId);

        return await query.OrderBy(t => t.Deadline).ToListAsync();
    }

    // Skapa uppgift
    public async Task AddAsync(StudyTask task)
    {
        using var db = await factory.CreateDbContextAsync();
        db.Tasks.Add(task);
        await db.SaveChangesAsync();
    }

    // Redigera uppgift
    public async Task UpdateAsync(StudyTask task)
    {
        using var db = await factory.CreateDbContextAsync();
        task.Course = null; // så att EF inte försöker uppdatera kursen också
        db.Tasks.Update(task);
        await db.SaveChangesAsync();
    }

    // Flytta uppgift till en annan kolumn
    public async Task MoveAsync(int id, KanbanStatus newStatus)
    {
        using var db = await factory.CreateDbContextAsync();
        var task = await db.Tasks.FindAsync(id);
        if (task is null) return;

        task.Status = newStatus;
        await db.SaveChangesAsync();
    }

    // Ta bort uppgift
    public async Task DeleteAsync(int id)
    {
        using var db = await factory.CreateDbContextAsync();
        await db.Tasks.Where(t => t.Id == id).ExecuteDeleteAsync();
    }

    // Hämta kurser till dropdown/filter på Kanban-sidan
    public async Task<List<Course>> GetCoursesAsync()
    {
        using var db = await factory.CreateDbContextAsync();
        return await db.Courses.OrderBy(c => c.Name).ToListAsync();
    }
}