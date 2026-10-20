using APR400___StudyFlow.Components;
using Microsoft.EntityFrameworkCore;
using APR400___StudyFlow.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Här kopplas SQLite-databsen
builder.Services.AddDbContextFactory<StudyFlowContext>(options =>
    options.UseSqlite("Data Source=studyflow.db"));

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