

using TeamsHubWebClient.DTOs;
using TeamsHubWebClient.Gateways.Interfaces;
using TeamsHubWebClient.SinglentonClasses;

public class TaskManagerRESTProvider : ITaskManager
{
    HttpClient clientServiceTask;
    ILogger<TaskManagerRESTProvider> _logger;

    public TaskManagerRESTProvider(ILogger<TaskManagerRESTProvider> logger, IHttpClientFactory httpClientFactory){
        _logger = logger;
        clientServiceTask = httpClientFactory.CreateClient("ApiGateWay");
    }

    public bool AddTask(TaskDTO newTask)
    {
        try
        {
            var result = clientServiceTask.PostAsJsonAsync($"/TeamHub/Task/", newTask).Result;
            result.EnsureSuccessStatusCode();
            var response = result.Content.ReadFromJsonAsync<Boolean>().Result;
            return response;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public List<TaskDTO> GetAllTaskByProject(int projectID)
    {
        try
        {
            var result = clientServiceTask.GetAsync($"/TeamHub/Task/{projectID}").Result;
            result.EnsureSuccessStatusCode();
            var response = result.Content.ReadFromJsonAsync<List<TaskDTO>>().Result;
            return response;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public List<TaskDTO> GetTaskbyDate(DateTime startDate, DateTime endDate)
    {
        try {                        
            var startDateFormat = $"{startDate.Year}-{startDate.Month:00}-{startDate.Day:00}";
            var endDateFormat = $"{endDate.Year}-{endDate.Month:00}-{endDate.Day:00}";
            var result = clientServiceTask.GetAsync($"/TeamHub/Task/{startDateFormat}/{endDateFormat}").Result;
            result.EnsureSuccessStatusCode();
            var response = result.Content.ReadFromJsonAsync<List<TaskDTO>>().Result;    
            return response;
        } catch (Exception e) {
            return null;
        }
    }

    public bool RemoveTask(int taskID)
    {
        return true;
    }

    public bool UpdateTask(TaskDTO task)
    {
        return true;
    }
}