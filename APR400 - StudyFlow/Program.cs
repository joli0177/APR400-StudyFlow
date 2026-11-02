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


// Här "Registrerar" man så appen kan hitta och sidor kan be om den för att hämta infroamtionen
builder.Services.AddScoped<TaskService>();  
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<ExaminationService>();
builder.Services.AddScoped<CourseService>();

var app = builder.Build();

// Allt ovanför här talar man om vad appen behöver
// Allt under här är hur appen ska köras 


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