using System.Globalization;

namespace APR400___StudyFlow.Services.Sorting;

// Jämför text i svensk bokstavsordning (Å, Ä, Ö hamnar efter Z) och bryr sig inte om stor/liten bokstav.
// static readonly: skapas en gång och delas av alla sorteringar.
public static class SwedishText
{
    public static readonly StringComparer Comparer =
        StringComparer.Create(new CultureInfo("sv-SE"), ignoreCase: true);
}