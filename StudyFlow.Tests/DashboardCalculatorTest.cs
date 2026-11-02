using System.Diagnostics;
using APR400___StudyFlow.Models;
using APR400___StudyFlow.Services;

namespace StudyFlow.Tests;

public class DashboardCalculatorTests
{
    // Ett fast datum, så att testerna ger samma svar oavsett när de körs
    private static readonly DateTime Today = new(2026, 11, 2);

    // Hjälpmetod för att snabbt skapa en uppgift X dagar från "idag"
    private static StudyTask CreateTask(string title, int daysFromToday, KanbanStatus status = KanbanStatus.ToDo) =>
        new() { Title = title, Deadline = Today.AddDays(daysFromToday), Status = status };

    // ================= BEHÖVER UPPMÄRKSAMHET (Focus) =================

    // Kontrollerar så dagens uppgift syns
    [Fact]
    public void GetFocusTasks_TaskDueToday_IsIncluded()
    {
        //arrange
        var tasks = new List<StudyTask> { CreateTask("Idag", 0) };
        
        // act
        var focus = DashboardCalculator.GetFocusTasks(tasks, Today);
        
        // assert
        Assert.Single(focus);
    }
    
    // Kontrollerar så en uppgift som gått över med en dag syns
    [Fact]
    public void GetFocusTasks_OverdueTask_IsIncluded()
    {
        // arrange: deadline var igår
        var tasks = new List<StudyTask> { CreateTask("Igår", -1) };

        // act
        var focus = DashboardCalculator.GetFocusTasks(tasks, Today);

        // assert
        Assert.Single(focus);
    }
    // Kontrollerar så en uppgift som ligger i done i kanban inte syns även om den är försenad
    [Fact]
    public void GetFocusTasks_DoneTask_IsNotIncluded()
    {
        // arrange: försenad men redan klar
        var tasks = new List<StudyTask> { CreateTask("Klar", -1, KanbanStatus.Done) };

        // act
        var focus = DashboardCalculator.GetFocusTasks(tasks, Today);

        // assert
        Assert.Empty(focus);
    }

    // ================= KOMMANDE (Upcoming) =================

    // kontrollerar så en uppgift som har dagens datum inte ligger under kommande utan under fokus
    [Fact]
    public void GetUpcomingTasks_TaskDueToday_IsNotIncluded()
    {
        // arrange: idag hör till Focus, inte Kommande
        var tasks = new List<StudyTask> { CreateTask("Idag", 0) };

        // act
        var upcoming = DashboardCalculator.GetUpcomingTasks(tasks, Today);

        // assert
        Assert.Empty(upcoming);
    }
    // kontrollerar så att en uppgift som ligger under klar i kanban inte syns
    [Fact]
    public void GetUpcomingTasks_DoneTask_IsNotIncluded()
    {
        // arrange: deadline imorgon men redan klar
        var tasks = new List<StudyTask> { CreateTask("Klar", 1, KanbanStatus.Done) };

        // act
        var upcoming = DashboardCalculator.GetUpcomingTasks(tasks, Today);

        // assert
        Assert.Empty(upcoming);
    }

    // kontrollerar så endast 5 uppgifter syns och inte mer 
    [Fact]
    public void GetUpcomingTasks_ReturnsMaxFive_SortedByDeadline()
    {
        // arrange: sju uppgifter i blandad ordning
        var tasks = new List<StudyTask>
        {
            CreateTask("Om 7", 7), CreateTask("Om 2", 2), CreateTask("Om 5", 5),
            CreateTask("Om 1", 1), CreateTask("Om 6", 6), CreateTask("Om 3", 3),
            CreateTask("Om 4", 4)
        };

        // act
        var upcoming = DashboardCalculator.GetUpcomingTasks(tasks, Today);

        // assert
        Assert.Equal(5, upcoming.Count);
        Assert.Equal("Om 1", upcoming[0].Title);   // närmast först
        Assert.Equal("Om 5", upcoming[4].Title);   // "Om 6" och "Om 7" kommer inte med
    }

    // ================= BÅDA TILLSAMMANS =================

    // Kontrollerar så om en uppgift finns i focus ska den inte synas i kommande och tvärtom
    [Fact]
    public void TaskDueToday_AppearsInFocusOnly_NotInBoth()
    {
        // arrange
        var tasks = new List<StudyTask> { CreateTask("Idag", 0) };

        // act
        var focus = DashboardCalculator.GetFocusTasks(tasks, Today);
        var upcoming = DashboardCalculator.GetUpcomingTasks(tasks, Today);

        // assert: den ska finnas på exakt ett ställe
        Assert.Single(focus);
        Assert.Empty(upcoming);
    }
}