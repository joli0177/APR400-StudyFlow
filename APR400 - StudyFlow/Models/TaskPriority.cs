namespace APR400___StudyFlow.Models;

// Prioritet som användaren väljer i formuläret.
// Normal = 0 är standard, så alla gamla uppgifter i databasen blir Normal efter migrationen.
public enum TaskPriority
{
    Normal,  // 0
    Medium,  // 1
    High     // 2
}