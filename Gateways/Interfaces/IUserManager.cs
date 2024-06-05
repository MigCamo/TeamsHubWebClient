using TeamsHubWebClient.DTOs;

namespace TeamsHubWebClient.Gateways.Interfaces 
{

    public interface IUserManager 
    {
        public List<User> getStudentsByProject(int idProject);
    }
}