using System.Net.Http.Headers;
using TeamsHubWebClient.DTOs;
using TeamsHubWebClient.Gateways.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddScoped<IUserIdentityManager, UserIdentityManagerRESTProvider>();
builder.Services.AddScoped<IProjectManager, ProjectManagerRESTProvider>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<HttpClientsAuthHelper>();


builder.Services.AddHttpClient("ApiGateWay", client => {
    var apiGateWayUrl = builder.Configuration.GetSection("Services")["apiGateWay"];
    client.BaseAddress = new Uri(apiGateWayUrl);
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
    if(startDate != null && endDate != null){
        return new { data = proyectManager.GetProjectsbyDate(startDate.Value, endDate.Value)};
    }
    else
    {
        return new { data = proyectManager.GetProjects(1)};
    }

}).WithName("myProjectsbyDate");

app.MapGet("/TeamHub/Projects/MyProjects/{studentID}", (IProjectManager proyectManager, int studentID) =>
{     
        return new { data = proyectManager.GetProjects(studentID)};

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

public sealed class HttpClientsAuthHelper : DelegatingHandler
{
    private readonly IHttpContextAccessor _accessor;

    public HttpClientsAuthHelper(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = _accessor.HttpContext.Request.HttpContext.Session.GetString("token_usuario");
        if (token != null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        else
        {
            Console.WriteLine("Token is null");
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
