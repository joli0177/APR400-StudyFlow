using APR400___StudyFlow.Models;

namespace APR400___StudyFlow.Services.Sorting;

// Viktigast först: försenade uppgifter överst, sedan närmast deadline, sedan titel.
// Det här är sorteringen med riktig logik, och den som motiverar att mönstret används.
public class PriorityFirstSort : ITaskSortStrategy
{
    public string Key => "priority";
    public string DisplayName => "Viktigast först";

    public IEnumerable<StudyTask> Sort(IEnumerable<StudyTask> tasks, DateTime today) =>
        tasks
            // true/false sorteras som false < true, så "Descending" lägger de försenade (true) först
            .OrderByDescending(t => t.IsOverdue(today))
            .ThenBy(t => t.Deadline)
            .ThenBy(t => t.Title, SwedishText.Comparer);
}