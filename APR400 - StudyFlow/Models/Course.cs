namespace APR400___StudyFlow.Models;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string CourseCode { get; set; } = "";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int StudyPace { get; set; } // gör så man kan lägga in 50% eller 100%

    // En kurs har många uppgifter
    public List<StudyTask> Tasks { get; set; } = new(); // En genereics lista då den ska innehålla uppgifter
}