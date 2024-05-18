
using TeamsHubWebClient.DTOs;

namespace TeamsHubWebClient.Gateways.Interfaces
{
    public interface IProjectManager
    {
        public bool AddProject(ProjectDTO project);
        public bool UpdateProject(ProjectDTO projectNew);
        public bool RemoveProject(ProjectDTO project);
        public  List<ProjectDTO> GetProjects(int studentID);
        public List<ProjectDTO> GetProjectsbyDate(DateTime startDate, DateTime endDate);
    }
}