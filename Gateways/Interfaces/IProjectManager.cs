
using TeamsHubWebClient.DTOs;

namespace TeamsHubWebClient.Gateways.Interfaces
{
    public interface IProjectManager
    {
        public bool AddProject(ProjectDTO project, int studentID);
        public bool UpdateProject(ProjectDTO projectNew);
        public bool RemoveProject(ProjectDTO project);
        public  List<ProjectDTO> GetAllMyProjects(int idStudent);
        public List<ProjectDTO> GetProjectsbyDate(DateTime startDate, DateTime endDate);
        public ProjectDTO GetProject(int IdProject);
        public List<TaskDTO> GetProjectTasksAsync(int idProject);
    }
}