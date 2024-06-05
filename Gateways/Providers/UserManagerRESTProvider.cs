using System.Text;
using TeamsHubWebClient.DTOs;
using TeamsHubWebClient.Gateways.Interfaces;

namespace TeamsHubWebClient.Gateways.Providers
{
    public class UserManagerRESTProvider : IUserManager
    {

        HttpClient clientServiceUser;
        ILogger<UserManagerRESTProvider> _logger;


        public UserManagerRESTProvider(ILogger<UserManagerRESTProvider> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            clientServiceUser = httpClientFactory.CreateClient("ApiGateWay");
        }

        public List<User> getStudentsByProject(int idProject)
        {
            List<User> response;
            try
            {
                 var result = clientServiceUser.GetAsync($"/TeamHub/Users/ByProject/{idProject}").Result;
                Console.WriteLine(result);
                result.EnsureSuccessStatusCode();
                response = result.Content.ReadFromJsonAsync<List<User>>().Result;
            }
            catch (System.Exception)
            {
                
                throw;
            }
            return response;
        }
    }
}
