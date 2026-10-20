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
    
    // Främmandde nyckel för att koplla kursen till uppgiften
    [Range(1, int.MaxValue, ErrorMessage = "Välj en kurs")]
    public int CourseId { get; set; }
    public Course?  Course { get; set; }
    
   
}