using APR400___StudyFlow.Models;

namespace APR400___StudyFlow.Services.Sorting;

// Först tillagd först (motsatsen till NewestFirstSort).
// Id används som "när den skapades". Fungerar så länge Id delas ut i stigande ordning,
// vilket SQLite gör. Vill man ha riktiga datum behövs en CreatedAt-kolumn och en migration.
public class OldestFirstSort : ITaskSortStrategy
{
    public string Key => "oldest";
    public string DisplayName => "Först tillagd";

    public IEnumerable<StudyTask> Sort(IEnumerable<StudyTask> tasks, DateTime today) =>
        tasks.OrderBy(t => t.Id);
}