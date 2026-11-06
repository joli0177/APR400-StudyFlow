using APR400___StudyFlow.Models;

namespace APR400___StudyFlow.Services.Sorting;

// Samma kurs bredvid varandra (sorterat på kurskod), och inom kursen närmast deadline först.
// Kräver att Course är inladdad, vilket TaskService.GetAllAsync gör med Include(t => t.Course).
public class CourseSort : ITaskSortStrategy
{
    public string Key => "course";
    public string DisplayName => "Kurs";

    public IEnumerable<StudyTask> Sort(IEnumerable<StudyTask> tasks, DateTime today) =>
        tasks
            // ?? "" om kursen av någon anledning saknas, så att sorteringen inte kraschar
            .OrderBy(t => t.Course?.CourseCode ?? "", SwedishText.Comparer)
            .ThenBy(t => t.Deadline);
}