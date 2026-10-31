// kunna ge en titel på en examination ett datum för den och sen själva typen detta för att kunna lägga till pch koplla till kursen
using System.ComponentModel.DataAnnotations;

namespace APR400___StudyFlow.Models;

public class Examination
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ange en titel")]
    public string Title { get; set; } = "";

    public DateTime Date { get; set; }

    public ExaminationType Type { get; set; } = ExaminationType.Exam;

    // Vilken kurs examinationen hör till
    [Range(1, int.MaxValue, ErrorMessage = "Välj en kurs")]
    public int CourseId { get; set; }
    public Course? Course { get; set; }
}