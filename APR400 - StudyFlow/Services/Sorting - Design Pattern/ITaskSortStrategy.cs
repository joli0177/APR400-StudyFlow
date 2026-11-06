using APR400___StudyFlow.Models;

namespace APR400___StudyFlow.Services.Sorting;

// Strategy-mönstret: kontraktet som alla sorteringar följer.
// Kanban känner bara till det här gränssnittet, aldrig de konkreta klasserna.
// En ny sortering = en ny klass + en rad i Program.cs. Kanban behöver inte ändras (Open/Closed).
public interface ITaskSortStrategy
{
    // Värdet i dropdownen, ska vara unikt (t.ex. "deadline")
    string Key { get; }

    // Texten användaren ser (t.ex. "Deadline (närmast först)")
    string DisplayName { get; }

    // "today" skickas in, precis som i DashboardCalculator, så att testerna kan bestämma datumet
    IEnumerable<StudyTask> Sort(IEnumerable<StudyTask> tasks, DateTime today);
}