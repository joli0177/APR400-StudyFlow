using APR400___StudyFlow.Models;

namespace APR400___StudyFlow.Services.Sorting;

// Närmast deadline först. Samma deadline: titel i bokstavsordning.
public class DeadlineSort : ITaskSortStrategy
{
    public string Key => "deadline";
    public string DisplayName => "Deadline (närmast först)";

    public IEnumerable<StudyTask> Sort(IEnumerable<StudyTask> tasks, DateTime today) =>
        tasks
            .OrderBy(t => t.Deadline)
            .ThenBy(t => t.Title, SwedishText.Comparer);
}