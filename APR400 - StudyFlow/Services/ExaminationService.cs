using Microsoft.EntityFrameworkCore;
using APR400___StudyFlow.Data;
using APR400___StudyFlow.Models;

namespace APR400___StudyFlow.Services;

public class ExaminationService(IDbContextFactory<StudyFlowContext> factory)
{
    // Hämtar kommande examinationer (från idag och framåt) med kursen inkluderad
    public async Task<List<Examination>> GetUpcomingAsync()
    {
        using var db = await factory.CreateDbContextAsync();
        var today = DateTime.Today;

        return await db.Examinations
            .Include(e => e.Course)
            .Where(e => e.Date >= today)
            .OrderBy(e => e.Date)
            .ToListAsync();
    }

    // Skapa examination
    public async Task AddAsync(Examination exam)
    {
        using var db = await factory.CreateDbContextAsync();
        db.Examinations.Add(exam);
        await db.SaveChangesAsync();
    }

    // Redigera examination
    public async Task UpdateAsync(Examination exam)
    {
        using var db = await factory.CreateDbContextAsync();
        exam.Course = null; // så att EF inte försöker uppdatera kursen också
        db.Examinations.Update(exam);
        await db.SaveChangesAsync();
    }

    // Ta bort examination
    public async Task DeleteAsync(int id)
    {
        using var db = await factory.CreateDbContextAsync();
        await db.Examinations.Where(e => e.Id == id).ExecuteDeleteAsync();
    }
}