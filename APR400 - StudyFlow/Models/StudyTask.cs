using System.ComponentModel.DataAnnotations;
namespace APR400___StudyFlow.Models;

public class StudyTask
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ange en titel")] // Anger att här måste det finnas en titel
    public string Title { get; set; } = "";
    
    public string? Description { get; set; } // Men frågetecknet innebär det att descreption får vara tomt
    public DateTime Deadline { get; set; }

    public KanbanStatus Status { get; set; } = KanbanStatus.ToDo;
    
	public TaskColor Color { get; set; } = TaskColor.Red;
    // Främmandde nyckel för att koplla kursen till uppgiften
    [Range(1, int.MaxValue, ErrorMessage = "Välj en kurs")]
    public int CourseId { get; set; }
    public Course?  Course { get; set; }
    
    // Sparas som siffra (precis som Color), se förklaringen om migrationen
    public TaskPriority Priority { get; set; } = TaskPriority.Normal;
    
    // Försenad = inte klar och deadline har passerat (idag räknas INTE som försenad).
    // En metod (inte en property) så att EF inte försöker spara den i databasen,
    // och så att "today" kan skickas in i testerna. Används av Kanban och PriorityFirstSort.
    public bool IsOverdue(DateTime today) =>
        Status != KanbanStatus.Done && Deadline.Date < today.Date;
    
   
}