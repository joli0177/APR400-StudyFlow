<h1>StudyFlow</h1>
Är en webbbaserad studieplanerare som ska hjälpa studenter att organisera och strukturrera upp sina kurser, examinationer och studieuppgifter
på ett och samma ställe. 
Studenter kan själva skapa de uppgifter som behövs genomföras för kursen. Exempelvis att läsa ett kapitel i kursliteraturen under veckan.
Varje uppgift kopplas till den specifika kursen för att skapa en tydlig sturkut. Detta är främst för studentern som läser kurser samtidigt. 

<h2>Funktiner</h2>
<h3>Dashborde - Startsidan</h3>
Dashboarden är hemsidans startsida och ger studenten en samlad översikt kring de aktuella studierna. Detta för att kunna planera sitt arbete för dagen och veckan. 
Här kan studenterna se:
 - Kurserna som pågår men också vilka som ska startas
 - Hur många uppgifter som ska göras
 - Vilka uppgifter som bör prioriteras samt kommande deadlines
 - Vilka examinationer som är inplanerade

<h3>Kanban</h3>
Kanban används för att struktura upp och för att skala ner de stora uppgiften till mindre och mer konkret vad måste göras.
Eftersom studneterna själva lägger in uppgifterna kan de bestämma hur detaljerade de vill vara, varje uppgift kopplas till en kurs, för att skapa enklare överblick. Detta eftersom studneterna kan välja att filterar ut kanban-tavlan på endast den kursen de vill se. 


<h3>Mina kurser</h3>
På mina kurser registrerar och hanterar studenenar sina kuser. Där de lägger in kursens namn, kurskod, startdatum, slutdatum och studietakt.
Examinationer skapas i dashboraden men kopplas till en kurs, vilket gör det möjligt att examinationen syns även på kursen i mina kurser. Detta eftersom när en examination har genomfröst kan studnerna gå till mina kurser för att se vad kursen hade för examinationsform. 

<h2>Teknik och ramverk</h2>
StudyFlow är utvecklat i C# och har anävnt följande teknik:
 - .NET 10 SDK - utevklingsplatformen
 - Blazor - Ramverk frö att bygga användargänsnittet
 - Enity Framework Core (EF Core) - Hantering av databasåtkomst
 - SQLite - Lokal databas för lagring av kurser, examinatoiner och uppgifter
 - xUnit - Enhetstester
 
<h2>Installation och komma igång</h2>
För att köra StudyFlow behöver du ha .Net 10 SDK installerat

<h3>1. Installera verktyget för databasen:</h3>
  - dotnet tool install --global dotnet-ef
  
<h3>2. Hämta projektet</h3>
  - git clone https://github.com/joli0177/APR400-StudyFlow.git
  - cd APR400-StudyFlow
  
<h3>3. Skapa databasen</h3>
  - cd "APR400 - StudyFlow"
  - dotnet ef database update
  
<h3>4. Starta appen</h3>
  - dotnet run
  
<h2>Viktigt information angående databasen</h2>
Då StuyFlow anävnder sig utav SQLite databas som är en lokal databas så kommer hemsidan första gången användare startar upp att vara tom.
Detta innebär att inga kurser, examinationer eller uppgifter syns, och användaren måste själv lägga till detta. 
Men detta är inte ett problem då StudyFlow är en egen baserad hemsida där användaren och studenterna själva väljer vad som ska läggas in. 
För att starta igång så börja med att Lägg till kurs under Mina kurser, så är det efter det enbart att börja lägga till uppgifter och examinationer. 
<h2>Enhetstester (Unit Tests)</h2>

Testerna i appen är främst fokus på dashboardens uppgiftöversikt, där testerna kontollerat vilka uppgifter som visas under **Behöver uppmärksamhet** samt **Kommande** .

Metoderna som testat är GetFocusTasks() och GetUpcomingTaks(). 
**För att köra testerna:**
  - cd ..
  - dotnet test


