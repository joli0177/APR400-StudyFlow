using APR400___StudyFlow.Models;

namespace APR400___StudyFlow.Services;

// Logiken för "Mina uppgifter" på dashboarden, utan databas, så att den går att unit-testa.
// "today" skickas in som parameter så att testerna kan bestämma vilket datum det är.
public static class DashboardCalculator
{
    public const int MaxUpcoming = 5;

    // Behöver uppmärksamhet: inte klara, och deadline idag eller tidigare
    public static List<StudyTask> GetFocusTasks(IEnumerable<StudyTask> tasks, DateTime today) =>
        tasks
            .Where(t => t.Status != KanbanStatus.Done && t.Deadline.Date <= today.Date)
            .OrderBy(t => t.Deadline)
            .ToList();

    // Kommande: inte klara, deadline från imorgon och framåt, max fem
    public static List<StudyTask> GetUpcomingTasks(IEnumerable<StudyTask> tasks, DateTime today) =>
        tasks
            .Where(t => t.Status != KanbanStatus.Done && t.Deadline.Date > today.Date)
            .OrderBy(t => t.Deadline)
            .Take(MaxUpcoming)
            .ToList();
}