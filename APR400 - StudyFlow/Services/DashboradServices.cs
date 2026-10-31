using Microsoft.EntityFrameworkCore;
using APR400___StudyFlow.Data;
using APR400___StudyFlow.Models;

namespace APR400___StudyFlow.Services;

public class DashboardService(IDbContextFactory<StudyFlowContext> factory)
{
    // Hämtar alla kurser med deras uppgifter, så kan dashboarden räkna ut allt den behöver
    public async Task<List<Course>> GetCoursesWithTasksAsync()
    {
        using var db = await factory.CreateDbContextAsync();
        return await db.Courses
            .Include(c => c.Tasks)
            .OrderBy(c => c.Name)
            .ToListAsync();
    }
}