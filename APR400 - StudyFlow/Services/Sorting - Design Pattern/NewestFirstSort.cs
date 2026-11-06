using APR400___StudyFlow.Models;

namespace APR400___StudyFlow.Services.Sorting;

// Senast tillagd först.
// Vi har ingen CreatedAt-kolumn, så vi använder Id: SQLite delar ut 1, 2, 3... i den ordning
// raderna skapas, så högst Id = senast tillagd. Det sparar en migration, men är ett antagande
// (se kommentaren i OldestFirstSort och reflektionen om CreatedAt).
public class NewestFirstSort : ITaskSortStrategy
{
    public string Key => "newest";
    public string DisplayName => "Senast tillagd";

    public IEnumerable<StudyTask> Sort(IEnumerable<StudyTask> tasks, DateTime today) =>
        tasks.OrderByDescending(t => t.Id);
}