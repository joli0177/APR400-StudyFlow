using APR400___StudyFlow.Models;

namespace APR400___StudyFlow.Services;

public class DashboardFacade
{
    private readonly DashboardService dashboardService;
    private readonly ExaminationService examinationService;

    public DashboardFacade(
        DashboardService dashboardService,
        ExaminationService examinationService)
    {
        this.dashboardService = dashboardService;
        this.examinationService = examinationService;
    }

    public async Task<DashboardData> GetDashboardDataAsync()
    {
        var today = DateTime.Today;
        
        var courses = await dashboardService.GetCoursesWithTasksAsync();
        var exams = await examinationService.GetUpcomingAsync();

        var allTasks = courses.SelectMany(c => c.Tasks).ToList();
        
        var activeCourses = courses
            .Where(c => c.EndDate.Date >= today)
            .OrderBy(c => c.EndDate)
            .ToList();

        return new DashboardData
        {
            Courses = courses,
            ActiveCourseList = activeCourses,
            Exams = exams,

            ActiveCourses = activeCourses.Count,
            TotalTasks = allTasks.Count,
            DoneTasks = allTasks.Count(t => t.Status == KanbanStatus.Done),
            TodoCount = allTasks.Count(t => t.Status == KanbanStatus.ToDo),
            InProgressCount = allTasks.Count(t => t.Status == KanbanStatus.InProgress),
            OverdueTasks = allTasks.Count(t => t.IsOverdue(today)),
            
            Focus = DashboardCalculator.GetFocusTasks(allTasks, today),
            Upcoming = DashboardCalculator.GetUpcomingTasks(allTasks, today)
        };
    }
    
    public Task<List<Examination>> GetUpcomingExamsAsync()
        => examinationService.GetUpcomingAsync();

    public Task AddExamAsync(Examination exam)
        => examinationService.AddAsync(exam);

    public Task UpdateExamAsync(Examination exam)
        => examinationService.UpdateAsync(exam);

    public Task DeleteExamAsync(int id)
        => examinationService.DeleteAsync(id);
}

public class DashboardData
{
    public List<Course> Courses { get; set; } = new();
    public List<Course> ActiveCourseList { get; set; } = new();
    public List<StudyTask> Focus { get; set; } = new();
    public List<StudyTask> Upcoming { get; set; } = new();
    public List<Examination> Exams { get; set; } = new();

    public int ActiveCourses { get; set; }
    public int TotalTasks { get; set; }
    public int DoneTasks { get; set; }
    public int TodoCount { get; set; }
    public int InProgressCount { get; set; }
    public int OverdueTasks { get; set; }
}