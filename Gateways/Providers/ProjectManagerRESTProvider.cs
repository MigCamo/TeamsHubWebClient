using TeamsHubWebClient.DTOs;
using TeamsHubWebClient.Gateways.Interfaces;

public class ProjectManagerRESTProvider : IProjectManager
{
    HttpClient clientServiceProjects;
    ILogger<ProjectManagerRESTProvider> _logger;

    public ProjectManagerRESTProvider(
        ILogger<ProjectManagerRESTProvider> logger,
        IHttpClientFactory httpClientFactory) 
    {
            _logger = logger;
            clientServiceProjects = httpClientFactory.CreateClient("ProjectService");
    } 

    public bool AddProject(ProjectDTO project)
    {
        try
        {
            var result = clientServiceProjects.PostAsJsonAsync($"/TeamHub/Projects/AddProject", project).Result;
            result.EnsureSuccessStatusCode();
            var response = result.Content.ReadFromJsonAsync<Boolean>().Result;
            return response;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public List<ProjectDTO> GetProjects()
    {
        try
        {
            var result = clientServiceProjects.GetAsync("/TeamHub/Projects/MyProjects").Result;
            result.EnsureSuccessStatusCode();
            var response = result.Content.ReadFromJsonAsync<List<ProjectDTO>>().Result;
            return response;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public List<ProjectDTO> GetProjectsbyDate(DateTime startDate, DateTime endDate)
    {
        try {                        
            var startDateFormat = $"{startDate.Year}-{startDate.Month:00}-{startDate.Day:00}";
            var endDateFormat = $"{endDate.Year}-{endDate.Month:00}-{endDate.Day:00}";
            var result = clientServiceProjects.GetAsync($"/TeamHub/Projects/MyProjects/{startDateFormat}/{endDateFormat}").Result;
            result.EnsureSuccessStatusCode();
            var response = result.Content.ReadFromJsonAsync<List<ProjectDTO>>().Result;    
            return response;
        } catch (Exception e) {
            return null;
        }
    }

    public bool RemoveProject(ProjectDTO project)
    {
        return true;
    }

    public bool UpdateProject(ProjectDTO projectNew)
    {
        try
        {
            _logger.LogInformation(projectNew.ToString()); 
            var result = clientServiceProjects.PutAsJsonAsync($"/TeamHub/Projects/UpdateProject", projectNew).Result;
            result.EnsureSuccessStatusCode();
            var response = result.Content.ReadFromJsonAsync<Boolean>().Result;
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.ToString());
            return false;
        }
    }
}