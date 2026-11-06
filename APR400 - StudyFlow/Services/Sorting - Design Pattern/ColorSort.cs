using APR400___StudyFlow.Models;

namespace APR400___StudyFlow.Services.Sorting;

// Sortera efter färg, i samma ordning som i TaskColor-enumen (Röd, Orange, Grön, Blå, Lila).
// Samma färg: närmast deadline först.
// Nyttigt om man använder färgerna som egen prioritet, t.ex. röd = viktigt.
public class ColorSort : ITaskSortStrategy
{
    public string Key => "color";
    public string DisplayName => "Färg";

    public IEnumerable<StudyTask> Sort(IEnumerable<StudyTask> tasks, DateTime today) =>
        tasks
            // Enum sorteras på sitt underliggande tal: Red = 0, Orange = 1 ... Purple = 4
            .OrderBy(t => t.Color)
            .ThenBy(t => t.Deadline);
}