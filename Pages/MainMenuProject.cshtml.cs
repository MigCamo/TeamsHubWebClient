

using Microsoft.AspNetCore.Mvc.RazorPages;
using TeamsHubWebClient.Gateways.Interfaces;

namespace TeamsHubWebClient.Pages
{
    public class MainMenuProject : PageModel
    {
         private readonly ILogger<IndexModel> _logger;

        private readonly IProjectManager _projectManager;

        public MainMenuProject(ILogger<IndexModel> logger, IProjectManager projectManager)
        {
            _logger = logger;
            _projectManager = projectManager;
        }  

        public void OnGet()
        {
       
        }

    }
}