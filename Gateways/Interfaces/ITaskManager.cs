

using TeamsHubWebClient.DTOs;

namespace TeamsHubWebClient.Gateways.Interfaces
{
    public interface ITaskManager
    {
        public bool AddTask(TaskDTO newTask);
        public bool UpdateTask(TaskDTO task);
        public bool RemoveTask(int taskID);
        public  List<TaskDTO> GetAllTaskByProject(int projectID);
        public List<TaskDTO> GetTaskbyDate(DateTime startDate, DateTime endDate);
    }
}