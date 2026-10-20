using APR400___StudyFlow.Components;
using Microsoft.EntityFrameworkCore;
using APR400___StudyFlow.Data;
using APR400___StudyFlow.Services;   
using APR400___StudyFlow.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Här kopplas SQLite-databsen
builder.Services.AddDbContextFactory<StudyFlowContext>(options =>
    options.UseSqlite("Data Source=studyflow.db"));

builder.Services.AddScoped<TaskService>();  

var app = builder.Build();

// Allt ovanför här talar man om vad appen behöver
// Allt under här är hur appen ska köras 

// DETTA ÄR TILLFÄLLIG DATA FÖR ATT TESTA VAD SOM HÄNDER
var dbFactory = app.Services.GetRequiredService<IDbContextFactory<StudyFlowContext>>();
using (var db = dbFactory.CreateDbContext())
{
    if (!db.Courses.Any())
    {
        db.Courses.AddRange(
            new Course { Name = "Webbutveckling", CourseCode = "APR400", StartDate = new DateTime(2026, 9, 1), EndDate = new DateTime(2027, 1, 15), StudyPace = 50 },
            new Course { Name = "Databaser", CourseCode = "DB200", StartDate = new DateTime(2026, 9, 1), EndDate = new DateTime(2026, 11, 1), StudyPace = 50 }
        );
        db.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS
    // value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();