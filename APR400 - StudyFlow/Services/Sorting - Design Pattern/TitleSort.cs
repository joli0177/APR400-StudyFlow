using APR400___StudyFlow.Models;

namespace APR400___StudyFlow.Services.Sorting;

// Titel A–Ö, med svensk bokstavsordning.
public class TitleSort : ITaskSortStrategy
{
    public string Key => "title";
    public string DisplayName => "Titel (A–Ö)";

    public IEnumerable<StudyTask> Sort(IEnumerable<StudyTask> tasks, DateTime today) =>
        tasks.OrderBy(t => t.Title, SwedishText.Comparer);
}