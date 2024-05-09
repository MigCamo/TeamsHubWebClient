using TeamsHubWebClient.DTOs;
using TeamsHubWebClient.Gateways.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddScoped<IUserIdentityManager, UserIdentityManagerRESTProvider>();
builder.Services.AddScoped<IProjectManager, ProjectManagerRESTProvider>();

builder.Services.AddHttpClient("UserIdentityService", client => {
    var userIdentityService = builder.Configuration.GetSection("Services")["userIdentityService"];
    client.BaseAddress = new Uri(userIdentityService);
    client.DefaultRequestHeaders.Add("accept", "application/json");
});

builder.Services.AddHttpClient("ProjectService", client => {
    var projectService = builder.Configuration.GetSection("Services")["projectService"];
    client.BaseAddress = new Uri(projectService);
    client.DefaultRequestHeaders.Add("accept", "application/json");    
});

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".TeamsHub.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.MapGet("/TeamHub/Projects/MyProjects/{startDate?}/{endDate?}", (IProjectManager proyectManager, DateTime? startDate, DateTime? endDate) =>
{     
    return new { data = proyectManager.GetProjectsbyDate(startDate.Value, endDate.Value)};
}).WithName("myProjectsbyDate");

app.MapGet("/TeamHub/Projects/MyProjects", (IProjectManager proyectManager) =>
{     
        return new { data = proyectManager.GetProjects()};
}).WithName("myProjects");

app.MapPost("/TeamHub/Projects/SaveProject", (IProjectManager proyectManager, ProjectDTO project) =>
{            
    bool result = false;

    if (project.IdProject == 0)
    {
        result = proyectManager.AddProject(project);
    }
    else
    {
        result = proyectManager.UpdateProject(project); 
    }   

    return result;       
}).WithName("SaveProject");

app.MapRazorPages();
app.Run();
